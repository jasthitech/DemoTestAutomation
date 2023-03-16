using DemoTestAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

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
        public static string toBillsAccount = "(//div[@class='card-0-5-61'])[2]";
        public static string inputAmmount = "//input[@name='amount']";
        public static string transferBtn = "//span[@class='Button-content-11-11__002e1__002e0']//span[text()='Transfer']";
        public static string transferSuccessMsg = "//span[@role='alert' and text()='Transfer successful']";
        public static string fromAccSearch = "//input[@placeholder='Search']";
        public static int initialEverydayAccBalance = 0;
        public static int initialBillsAccBalance = 0;


        public static void VerifyPaymentsPanelDisplayed(WebDriver webDriver)
        {
            IWebElement webElement = webDriver.FindElement(By.XPath(paymentsPanel));
            bool isDisplayed = webElement.Displayed;
            Assert.IsTrue(isDisplayed);
        }

        public static void ClickOnChooseAccount(WebDriver webDriver)
        {
            webDriver.FindElement(By.XPath(paymentsPanel)).Click();            
            bool isDisplayed = Webdriverhelper.RetryIsDisplayed(webDriver, everydayAccLabel);
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

        public static void SelectEverydayAccountAsFromAcc(WebDriver webDriver, int transferAmount)
        {
            try
            {                
                  Webdriverhelper.RetryClick(webDriver, selectEverydayAcc);              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                       
        }

        public static void ClickOnChooseToAccount(WebDriver webDriver)
        {           
            Webdriverhelper.RetryClick(webDriver, chooseToAccount);
        }

        public static void ClickOnAccountsTab(WebDriver webDriver)
        {
            IWebElement element = webDriver.FindElement(By.Id(accountsTabId));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
            executor.ExecuteScript("arguments[0].click();", element);            
        }

        public static void ChooseBillAccAsToAcc(WebDriver webDriver)
        {            
            IWebElement element = webDriver.FindElement(By.XPath(toBillsAccount));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)webDriver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public static void SetFocusAndEnterAmount(WebDriver webDriver, int amount)
        {
            webDriver.FindElement(By.XPath(inputAmmount)).SendKeys(amount.ToString());
        }

        public static void ClickOnTransferBtn(WebDriver webDriver)
        {
            webDriver.FindElement(By.XPath(transferBtn)).Click();
        }

        public static void VerifyTransferSuccessMsg(WebDriver webDriver)
        {
            string message = Webdriverhelper.RetryDynamicElementsWithMsg(webDriver, transferSuccessMsg);
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
