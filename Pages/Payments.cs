using DemoTestAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace DemoTestAutomation.Pages
{
    public static class Payments
    {
        public static string paymentsPanel = "//span[text()='Choose account']";
        public static string chooseToAccount = "//button[@data-monitoring-label='Transfer Form To Chooser']";
        public static string everydayAccLabel = "//p[text()='Everyday']";        
        public static string everydayAccBalance = "(//button[@class='button-0-5-60'])[2]";        
        public static string selectEverydayAcc = "(//button[@class='button-0-5-60'])[2]";
        public static string billsAccBalance = "(//button[@class='button-0-5-60'])[1]";
        public static string accountsTab = "//li[@data-testid='to-account-accounts-tab']";
        public static string accountsTabId = "react-tabs-2";
        public static string toAccountsSearch = "//input[@data-monitoring-label='Transfer Form Search']";
        //public static string toBillsAccount = "(//div[@class='card-0-5-61'])[2]";
        public static string toBillsAccount = "(//button[@data-monitoring-label='Transfer Form Account Card']//div//div[@class='card-0-5-61'])";
        public static string toBillsAccByCss = "button[data-monitoring-label='Transfer Form Account Card']";
        public static string inputAmmount = "//input[@name='amount']";
        public static string transferBtn = "//span[@class='Button-content-11-11__002e1__002e0']//span[text()='Transfer']";
        public static string transferSuccessMsg = "//span[@role='alert' and text()='Transfer successful']";
        public static string fromAccSearch = "//input[@placeholder='Search']";
        public static string billsChoosedintoToAcc = "//button[contains(@data-testid, 'to-account-chooser')]//div//div//div//p[contains(text(), 'Bills ')]";
        public static int initialEverydayAccBalance = 0;
        public static int initialBillsAccBalance = 0;


        public static void VerifyPaymentsPanelDisplayed(WebDriver webDriver)
        {
            IWebElement webElement = webDriver.FindElement(By.XPath(paymentsPanel));
            bool isDisplayed = webElement.Displayed;
            Assert.IsTrue(isDisplayed);
        }

        public static void ClickOnChooseAccount(WebDriver webDriver, WebDriverWait wait)
        {
            webDriver.FindElement(By.XPath(paymentsPanel)).Click();
            // Wait for the web element to be visible before clicking on it
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(everydayAccLabel)));
            bool isDisplayed = webDriver.FindElement(By.XPath(paymentsPanel)).Displayed;
            Assert.IsTrue(isDisplayed);
        }

        public static void ValidateEverydayAccBalance(WebDriver webDriver, int transferAmount)
        {
            string balance = webDriver.FindElement(By.XPath(everydayAccBalance)).Text;
            //string input = "Everyday\r\n2,500.00 Avl.\r\n02-0000-0000000-001";
            string[] parts = balance.Replace("\r\n", " ").Split();
            string numericString = new string(parts[1].Where(char.IsDigit).ToArray());
            float amount = float.Parse(numericString) / 100;            
            //storing value in a constant for later use
            initialEverydayAccBalance = Convert.ToInt32(amount);
            
            Assert.IsTrue(initialEverydayAccBalance > transferAmount);                      
        }

        public static void VerifyBillsAccountBalance(WebDriver webDriver, int transferAmount)
        {
            string balance = webDriver.FindElement(By.XPath(billsAccBalance)).Text;
            //string input = "Everyday\r\n2,500.00 Avl.\r\n02-0000-0000000-001";
            string[] parts = balance.Replace("\r\n", " ").Split();
            string numericString = new string(parts[1].Where(char.IsDigit).ToArray());
            float amount = float.Parse(numericString) / 100;            
            initialBillsAccBalance = Convert.ToInt32(amount);
            Assert.IsTrue(initialBillsAccBalance < transferAmount);                        
        }

        public static void SelectEverydayAccountAsFromAcc(WebDriver webDriver, int transferAmount, WebDriverWait wait)
        {
            // Wait for the web element to be visible before clicking on it
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(selectEverydayAcc)));

            // Wait for the web element to be clickable
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(selectEverydayAcc)));
            IWebElement element = webDriver.FindElement(By.XPath(selectEverydayAcc));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
            executor.ExecuteScript("arguments[0].click();", element);           
        }

        public static void ClickOnChooseToAccount(WebDriver webDriver, WebDriverWait wait)
        {           
            IWebElement element = webDriver.FindElement(By.XPath(chooseToAccount));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public static void ClickOnAccountsTab(WebDriver webDriver)
        {
            IWebElement element = webDriver.FindElement(By.Id(accountsTabId));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
            executor.ExecuteScript("arguments[0].click();", element);            
        }

        public static void ChooseBillAccAsToAcc(WebDriver webDriver, WebDriverWait wait)
        {
            try
            {
                ReuseAppActions.FindByCssAndClickByJavaScript(webDriver, toBillsAccByCss, wait);
                bool isBillsAccSelected = webDriver.FindElement(By.XPath(billsChoosedintoToAcc)).Displayed;
                Assert.IsTrue(isBillsAccSelected);
            }
            catch(Exception ex)
            {
                //Trying to select the Bills account with JavaScript Executer if Click failes
                    IWebElement css_element = webDriver.FindElement(By.CssSelector(toBillsAccByCss));
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
                    executor.ExecuteScript("arguments[0].click();", css_element);
            }            
        }

        public static void SetFocusAndEnterAmount(WebDriver webDriver, int amount)
        {
            webDriver.FindElement(By.XPath(inputAmmount)).SendKeys(amount.ToString());
        }

        public static void ClickOnTransferBtn(WebDriver webDriver)
        {            
            IWebElement element = webDriver.FindElement(By.XPath(transferBtn));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public static void VerifyTransferSuccessMsg(WebDriver webDriver, WebDriverWait wait)
        {
            string message = ReuseAppActions.RetryDynamicElementsWithMsg(webDriver, transferSuccessMsg, wait);
            Assert.IsTrue(message.Contains("Transfer successful"));            
        }

        public static void VerifyBalanceOfEverydayAccountAfterTransfer(WebDriver webDriver, int transferAmount)
        {
            string balance = webDriver.FindElement(By.XPath(everydayAccBalance)).Text;
            //string input = "Everyday\r\n2,500.00 Avl.\r\n02-0000-0000000-001";
            string[] parts = balance.Replace("\r\n", " ").Split();
            string numericString = new string(parts[1].Where(char.IsDigit).ToArray());
            float amount = float.Parse(numericString) / 100;
            //float amount = float.Parse(balance.Split()[1].Replace(",", ""));
            int currentBalance = Convert.ToInt32(amount);
            int expectedBalanceAfterTransfer = initialEverydayAccBalance - transferAmount;
            Assert.IsTrue(currentBalance == expectedBalanceAfterTransfer);                      
        }

        public static void VerifyBalanceOfBillsAccountAfterTransfer(WebDriver webDriver, int receivedAmount)
        {
            string balance = webDriver.FindElement(By.XPath(billsAccBalance)).Text;
            //string input = "Everyday\r\n2,500.00 Avl.\r\n02-0000-0000000-001";
            string[] parts = balance.Replace("\r\n", " ").Split();
            string numericString = new string(parts[1].Where(char.IsDigit).ToArray());
            float amount = float.Parse(numericString) / 100;
            //float amount = float.Parse(balance.Split()[1].Replace(",", ""));
            int currentBalance = Convert.ToInt32(amount);
            int expectedBalanceAfterTransfer = initialBillsAccBalance + receivedAmount;
            Assert.IsTrue(currentBalance == expectedBalanceAfterTransfer);
        }
    }

    
}
