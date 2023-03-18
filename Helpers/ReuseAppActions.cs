using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DemoTestAutomation.Helpers
{
    public static class ReuseAppActions
    {
        public static void ClickOnMainMenu(WebDriver webDriver, WebDriverWait wait)
        {            
            RetryClick(webDriver, Pages.Home.mainMenu, wait);
        }

        public static void ClickOnMainMenuPayees(WebDriver webDriver, WebDriverWait wait)
        {           
            RetryClick(webDriver, Pages.Home.mainMenuPayees, wait);
        }

        public static void ClickOnMainMenuPayOrTransfer(WebDriver webDriver, WebDriverWait wait)
        {            
            RetryClick(webDriver, Pages.Home.mainMenuPayOrTransfer, wait);
        }

        public static void RetryClick(WebDriver webDriver, string web_element, WebDriverWait wait)
        {
            int retryCount = 0;           
            while (retryCount <= 5)
            {
                try
                {
                    // Find the web element                    
                    IWebElement webElement = webDriver.FindElement(By.XPath(web_element));
                    // Set focus to the web element
                    // webDriver.SwitchTo().ActiveElement().SendKeys("");

                    // Wait for the web element to be visible before clicking on it
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(web_element)));

                    // Wait for the web element to be clickable
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(web_element)));

                    // Click the Web element
                    webElement.Click();                    
                    break;
                }
                catch (NoSuchElementException)
                {
                    // Web element not found, wait and try again
                    System.Threading.Thread.Sleep(200);
                    retryCount++;
                }
            }
        }        

        public static string RetryDynamicElementsWithMsg(WebDriver webDriver, string web_element, WebDriverWait wait)
        {
            // Find the Web element
            int retryCount = 0;
            string elementText = "";
            while (retryCount <= 10 && string.IsNullOrEmpty(elementText))
            {
                try
                {
                    // Find the web element
                    IWebElement webElement = webDriver.FindElement(By.XPath(web_element));

                    // Wait for the web element to be visible before clicking on it
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(web_element)));

                    // Get the text of the alert element
                    elementText = webElement.Text;

                    // Output the element text to the console
                    Console.WriteLine("text: " + elementText);
                }
                catch (NoSuchElementException)
                {
                    // Web element not found, wait and try again
                    System.Threading.Thread.Sleep(200);
                    retryCount++;
                }
            }
            return elementText;
        }

        public static void FindByCssAndClickByJavaScript(WebDriver webDriver, string element, WebDriverWait wait)
        {
            try
            {
                IWebElement web_element = webDriver.FindElement(By.CssSelector(element));

                // Wait for the web element to be visible before clicking on it
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(element)));

                // Wait for the web element to be clickable
                wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(element)));

                
                IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
                executor.ExecuteScript("arguments[0].click();", web_element);
            }
            catch (Exception ex)
            {
                TakeScreenshot(webDriver, element);
            }
        }

        public static void FindByXpathAndClickByJavaScript(WebDriver webDriver, string element, WebDriverWait wait)
        {
            try
            {
                IWebElement web_element = webDriver.FindElement(By.XPath(element));

                // Wait for the web element to be visible before clicking on it
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(element)));

                // Wait for the web element to be clickable
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(element)));


                IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
                executor.ExecuteScript("arguments[0].click();", web_element);
            }
            catch (Exception ex)
            {
                TakeScreenshot(webDriver, element);
            }
        }

        // TakeScreenshot method to capture screenshot
        public static void TakeScreenshot(IWebDriver driver, string screenshotName)
        {
            // capture the screenshot as a file
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            // save the screenshot to the given file name and location
            string fileName = Path.Combine(Directory.GetCurrentDirectory(), $"{screenshotName}.png");
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
        }

    }
}
