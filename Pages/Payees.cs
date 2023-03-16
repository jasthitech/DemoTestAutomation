using DemoTestAutomation.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestAutomation.Pages
{
    public static class Payees
    {
        public static string payeeHeading = "//h1[@class='CustomPage-heading']//span[text()='Payees']";
        public static string payeeHeadingText = "Payees";
        public static string add_btn = "//button[@aria-label='Add payee']//span[text()='Add']";
        public static string add_Payee_Model = "//div[@class='js-modal-inner Modal-content']";
        public static string add_Payee_Model_Input_Name = "//input[@name='apm-name']";
        public static string add_Payee_Model_Input_Particulars_One = "//input[@id='apm-that-particulars']";
        public static string add_Payee_Model_Input_Particulars_Two = "//input[@id='apm-that-code']";
        public static string add_Payee_Model_Input_Add_Btn = "//button[@class='js-submit Button Button--primary']";
        public static string add_Payee_Input_Blank_Model_Add_Btn = "//button[@type='button' and text()='Add']";
        public static string check_Newly_Added_Payee = "//*[@class='js-payee-name' and text()='AUCKLAND CANOE CLUB']";
        public static string select_Payee_From_Model_List = "//span[@title='AUCKLAND CANOE CLUB' and text()='AUCKLAND CANOE CLUB']";
        public static string check_Payee_Added_Msg = "//span[@role='alert' and text()='Payee added']";
        public static string check_Newly_Added_Payee_InList = "//span[@class='js-payee-name' and text()='AUCKLAND CANOE CLUB']";
        public static string add_Payee_Model_ErrorMsg = "//div[@class='error-header' and text()='A problem was found. Please correct the field highlighted below.']";
        public static string existing_Payees_List = "//div[@class='Row js-payee-item']";
        public static string payees_List_Name_Column = "//h3[@role='button']//span[text()='Name']";
        //Keeping below example Xpath for future references.
        //public static string select_Payee_Added_Msg = "//div[contains(@class, 'message') and contains(text(), 'Payee added')]";

        public static void VerifyPayeesPageHeading(WebDriver webDriver)
        {
            var headingText = webDriver.FindElement(By.XPath(Pages.Payees.payeeHeading)).GetCssValue;
            var headingTextValue = webDriver.FindElement(By.XPath(Pages.Payees.payeeHeading)).Text;
            Assert.IsNotNull(headingText);
            Assert.IsTrue(headingTextValue == Pages.Payees.payeeHeadingText);
        }

        public static void NavigateToPayeesPage(WebDriver webDriver)
        {
            Helpers.Webdriverhelper.NavigateToPage(webDriver,
                Helpers.Webdriverhelper.GetBaseUrl());
            ReuseAppActions.ClickOnMainMenu(webDriver);
            ReuseAppActions.ClickOnMainMenuPayees(webDriver);
        }

        public static void ClickOnPayeeAddBtn(WebDriver webDriver)
        {
            webDriver.FindElement(By.XPath(add_btn)).Click();
            //check model is displayed
            Assert.IsTrue(webDriver.FindElement(By.XPath(add_Payee_Model)).Displayed);         
        }

        public static void EnterPayeeDetails(WebDriver webDriver)
        {
            webDriver.FindElement(By.XPath(add_Payee_Model_Input_Name)).SendKeys("AUCKLAND CANOE CLUB");
            //Select Payee name from the list 
            webDriver.FindElement(By.XPath(select_Payee_From_Model_List)).Click();
            webDriver.FindElement(By.XPath(add_Payee_Model_Input_Particulars_One)).SendKeys("Jason RT");
            webDriver.FindElement(By.XPath(add_Payee_Model_Input_Particulars_Two)).SendKeys("Subscription");            
        }

        public static void ClickOnPayeeModelAddBtn(WebDriver webDriver)
        {
            webDriver.FindElement(By.XPath(add_Payee_Input_Blank_Model_Add_Btn)).Click();            
        }

        public static void CheckForPayeeAddedMsg(WebDriver webDriver)
        {
            // Find the alert element
            int retryCount = 0;
            string alertText = "";
            while (retryCount <= 10 && string.IsNullOrEmpty(alertText))
            {
                try
                {
                    // Find the alert element
                    IWebElement alertElement = webDriver.FindElement(By.XPath(check_Payee_Added_Msg));

                    // Get the text of the alert element
                    alertText = alertElement.Text;

                    // Output the alert text to the console
                    Console.WriteLine("Alert text: " + alertText);
                }
                catch (NoSuchElementException)
                {
                    // Alert element not found, wait and try again
                    System.Threading.Thread.Sleep(200);
                    retryCount++;
                }
            }
            bool isDisplayed = webDriver.FindElement(By.XPath(check_Payee_Added_Msg)).Displayed;
            Assert.IsTrue(isDisplayed);            
        }

        public static void CheckForNewlyAddedPayeeInTheList(WebDriver webDriver)
        {
            bool added = webDriver.FindElement(By.XPath(check_Newly_Added_Payee_InList)).Displayed;
            Assert.IsTrue(added);            
        }

        public static void isValidationErrorDisplayedOnPayeeMdl(WebDriver webDriver)
        {
            string msg = Webdriverhelper.RetryDynamicElementsWithMsg(webDriver, add_Payee_Model_ErrorMsg);
            Assert.IsTrue(msg.Contains("A problem was found. Please correct the field highlighted below."));
            bool isDisplayed = webDriver.FindElement(By.XPath(add_Payee_Model_ErrorMsg)).Displayed;
            Assert.IsTrue(isDisplayed);
        }

        public static void isValidationErrorNotDisplayedOnPayeeMdl(WebDriver webDriver)
        {
            // Find the element using the given XPath
            var element = new List<IWebElement>();
            element = webDriver.FindElements(By.XPath(add_Payee_Model_ErrorMsg)).ToList();
            Assert.IsTrue(element.Count <= 0);

            //****************Useful logic Keeping here for now*******************
            /*try
            {
                element = webDriver.FindElements(By.XPath(add_Payee_Model_ErrorMsg)).ToList();
                Assert.IsTrue(element.Count <= 0);
            }
            catch (NoSuchElementException)
            {
                Assert.IsTrue(element.Count <= 0);
                //Assert.IsNull(element); //div[@class='Row js-payee-item']
            }*/

        }

        public static void VerifyPayeeListHasExistingPayees(WebDriver webDriver)
        {
            var element = new List<IWebElement>();
            element = webDriver.FindElements(By.XPath(existing_Payees_List)).ToList();
            Assert.IsTrue(element.Count >= 1);
        }

        public static void FillInPayeeDetailsToValidateSorting(WebDriver webDriver)
        {
            webDriver.FindElement(By.XPath(add_Payee_Model_Input_Name)).SendKeys("AUCKLAND CANOE CLUB");
            //Select Payee name from the list 
            webDriver.FindElement(By.XPath(select_Payee_From_Model_List)).Click();
            webDriver.FindElement(By.XPath(add_Payee_Model_Input_Particulars_One)).SendKeys("Jason RT");
            webDriver.FindElement(By.XPath(add_Payee_Model_Input_Particulars_Two)).SendKeys("Subscription");
        }

        public static void VerifyNewlyAddedPayeeIsAtTopOfList(WebDriver webDriver)
        {
            var elements = new List<IWebElement>();
            elements = webDriver.FindElements(By.XPath(existing_Payees_List)).ToList();
            Assert.IsTrue(elements.Count >= 1);
            string newPayee = elements.First().Text;
            Assert.IsTrue(newPayee.Contains("AUCKLAND CANOE CLUB"));            
        }

        public static void SortNewlyAddedPayeeByName(WebDriver webDriver)
        {
            var elements = new List<IWebElement>();
            elements = webDriver.FindElements(By.XPath(existing_Payees_List)).ToList();
            Assert.IsTrue(elements.Count >= 1);
            webDriver.FindElement(By.XPath(payees_List_Name_Column)).Click();
            var elementsAfterSorting = new List<IWebElement>();
            elementsAfterSorting = webDriver.FindElements(By.XPath(existing_Payees_List)).ToList();
            Assert.IsTrue(elements.Count >= 1);
            var firstElementBeforeSort=elements.First().Text;
            var lastElementAfterSort = elementsAfterSorting.Last().Text;
            //first element before sorting now became last element, hence sorting working
            Assert.IsTrue(firstElementBeforeSort.Contains(lastElementAfterSort));          
            
        }
    }
}
