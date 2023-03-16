namespace DemoTestAutomation.Helpers
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;

    public static class Webdriverhelper
    {
        public static string baseUrl;
        public static string browserType;
        public static string chromeDriverFullPath;
        public static string chromeDriverPath;
        public static string geckoDriverPath;
        public static WebDriver driver;
        public static WebDriverWait wait;
        static Webdriverhelper()
        {
            baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            browserType = ConfigurationManager.AppSettings["Browser"];
            string chrmDriverPathSysPath = ConfigurationManager.AppSettings["ChrmDriverPathForSysPath"];
            chromeDriverPath = ConfigurationManager.AppSettings["ChromeDriverPath"];
            geckoDriverPath = ConfigurationManager.AppSettings["geckoDriverPath"];

            // Get the path of the project folder
            string projectFolderPath = Directory.GetCurrentDirectory();

            // Combine the project folder path with chromedriver.exe filename to get the full path
            chromeDriverFullPath = Path.Combine(projectFolderPath, chrmDriverPathSysPath);

            // Get the current system PATH environment variable value

            //Set driver in the path            
            string systemPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
            string[] paths = systemPath.Split(Path.PathSeparator);

            if (!paths.Contains(chromeDriverFullPath))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Environment.SetEnvironmentVariable("PATH", $"{systemPath};{chromeDriverFullPath}", EnvironmentVariableTarget.Machine);
                }
                else
                {
                    Console.WriteLine("Adding to the system PATH is not supported on this platform.");
                }
            }
        }

        public static WebDriver GetWebDriver()
        {           
            switch (browserType)
            {
                case "Chrome":
                    var options = new ChromeOptions();
                    options.BinaryLocation = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"; // Set the path to the Chrome executable
                    options.AddArguments("--start-maximized"); // Add any other desired Chrome options
                    var service = ChromeDriverService.CreateDefaultService(chromeDriverFullPath); // Set the path to the ChromeDriver executable
                    driver = new ChromeDriver(service, options);                                        
                    break;
                case "Firefox":
                    driver = new ChromeDriver(geckoDriverPath);                    
                    break;
                // Add other cases for other browsers if needed
                default:
                    throw new ArgumentException("Invalid browser type specified in Config.xml");
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            // Set up a WebDriverWait object with a timeout of 10 seconds
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            return driver;
        }

        public static string GetBaseUrl()
        {
            return baseUrl;
        }

        public static void WaitForPageLoad(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void NavigateToPage(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitForPageLoad(driver);
        }

        public static void RetryClick(WebDriver webDriver, string web_element)
        {            
            int retryCount = 0;
            bool isDisplayed = false;
            bool isEnabled = false;      
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

        public static bool RetryIsDisplayed(WebDriver webDriver, string web_element)
        {
            // Find the Web element
            int retryCount = 0;
            bool isDisplayed = false;
            while (retryCount <= 5 && isDisplayed==false)
            {
                try
                {
                    // Find the web element
                    IWebElement webElement = webDriver.FindElement(By.XPath(web_element));

                    // Get the text of the Web element
                    isDisplayed = webElement.Displayed;

                    // Output the element status to the console
                    Console.WriteLine("Element Display Status: " + isDisplayed);
                }
                catch (NoSuchElementException)
                {
                    // Web element not found, wait and try again
                    System.Threading.Thread.Sleep(200);
                    retryCount++;
                }
            }
            return isDisplayed;
        }

        public static string RetryDynamicElementsWithMsg(WebDriver webDriver, string web_element)
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

        public static void RetryCheckElementNotPresent(WebDriver webDriver, string web_element)
        {
            // Find the web element
            int retryCount = 0;
            bool webElement = false;
            while (retryCount <= 1 && webElement == false)
            {
                try
                {
                    // Find the element
                    webElement = webDriver.FindElement(By.XPath(web_element)).Displayed;

                    // Output the alert text to the console
                    Console.WriteLine("Is Element Displayed: " + webElement);
                }
                catch (NoSuchElementException)
                {
                    // element not found, wait and try again
                    System.Threading.Thread.Sleep(200);
                    retryCount++;
                }
            }
            Assert.IsFalse(webElement);
        }

        public static void Quit()
        {
            driver.Quit();
        }
    }

}
