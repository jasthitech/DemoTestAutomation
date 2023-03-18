using DemoTestAutomation.Helpers;
using DemoTestAutomation.Pages;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gherkin;

namespace DemoTestAutomation.StepDefinitions
{
    [Binding]
    public class PayeesFunctionalityStepDefinitions
    {
        private readonly TestContext _testContext;        
        public Webdriverhelper driverhelper;
        public PayeesFunctionalityStepDefinitions(TestContext testContext) // use it as ctor parameter
        {
            _testContext = testContext;
            driverhelper = new Webdriverhelper();
        }


        [Given(@"I am on the BNZ demo application home page")]
        public void GivenIAmOnTheBNZDemoApplicationHomePage()
        {
            driverhelper.NavigateToPage(driverhelper.GetWebDriver(),
                driverhelper.GetBaseUrl());
        }

        [When(@"I click on the '([^']*)' option")]
        public void WhenIClickOnTheOption(string menu)
        {
            WebDriver webDriver = driverhelper.driver;
            ReuseAppActions.ClickOnMainMenu(webDriver, driverhelper.wait); ;
        }

        [When(@"I click on the '([^']*)' option in the navigation menu")]
        public void WhenIClickOnTheOptionInTheNavigationMenu(string payees)
        {
            WebDriver webDriver = driverhelper.driver;
            ReuseAppActions.ClickOnMainMenuPayees(webDriver, driverhelper.wait);
        }

        [Then(@"the Payees page should be loaded successfully")]
        public void ThenThePayeesPageShouldBeLoadedSuccessfully()
        {
            WebDriver webDriver = driverhelper.driver;
            Payees.VerifyPayeesPageHeading(webDriver);
        }

        [Given(@"I am on the Payees page")]
        public void GivenIAmOnThePayeesPage()
        {
            WebDriver webDriver = driverhelper.GetWebDriver();
            Payees.NavigateToPayeesPage(driverhelper, driverhelper.baseUrl, driverhelper.wait);
            Payees.VerifyPayeesPageHeading(webDriver);
        }

        [When(@"I click the '([^']*)' button")]
        public void WhenIClickTheButton(string add)
        {
            Pages.Payees.ClickOnPayeeAddBtn(driverhelper.driver);
        }

        [When(@"I enter the payee details \(name, account number\)")]
        public void WhenIEnterThePayeeDetailsNameAccountNumber()
        {
            Pages.Payees.EnterPayeeDetails(driverhelper.driver);
        }

        [When(@"I click the '([^']*)' button on the model")]
        public void WhenIClickTheButtonOnTheModel(string add)
        {
            Pages.Payees.ClickOnPayeeModelAddBtn(driverhelper.driver);
        }

        [Then(@"I should see the message '([^']*)'")]
        public void ThenIShouldSeeTheMessage(string p0)
        {
            Pages.Payees.CheckForPayeeAddedMsg(driverhelper.driver);
        }

        [Then(@"the payee should be added to the list of payees")]
        public void ThenThePayeeShouldBeAddedToTheListOfPayees()
        {
            Pages.Payees.CheckForNewlyAddedPayeeInTheList(driverhelper.driver);
        }

        //Below Scenario for Verifying payee name is a required field

        [Then(@"I should see validation errors")]
        public void ThenIShouldSeeValidationErrors()
        {
            Pages.Payees.isValidationErrorDisplayedOnPayeeMdl(driverhelper.driver, driverhelper.wait);
        }

        [When(@"I populate the mandatory fields")]
        public void WhenIPopulateTheMandatoryFields()
        {
            Pages.Payees.EnterPayeeDetails(driverhelper.driver);
        }

        [Then(@"the validation errors should disappear")]
        public void ThenTheValidationErrorsShouldDisappear()
        {
            Pages.Payees.isValidationErrorNotDisplayedOnPayeeMdl(driverhelper.driver);
        }

        [Given(@"there are existing payees in the list")]
        public void GivenThereAreExistingPayeesInTheList()
        {
            Pages.Payees.VerifyPayeeListHasExistingPayees(driverhelper.driver);
        }

        [When(@"the user adds a new payee")]
        public void WhenTheUserAddsANewPayee()
        {
            Pages.Payees.ClickOnPayeeAddBtn(driverhelper.driver);
            Pages.Payees.EnterPayeeDetails(driverhelper.driver);
            Pages.Payees.ClickOnPayeeModelAddBtn(driverhelper.driver);
            Pages.Payees.CheckForNewlyAddedPayeeInTheList(driverhelper.driver);
        }

        [Then(@"the payee list should be sorted in ascending order by default")]
        public void ThenThePayeeListShouldBeSortedInAscendingOrderByDefault()
        {
            Pages.Payees.VerifyNewlyAddedPayeeIsAtTopOfList(driverhelper.driver);
        }

        [Then(@"the user clicks the Name header")]
        public void ThenTheUserClicksTheNameHeader()
        {
            driverhelper.driver.FindElement(By.XPath(Payees.payees_List_Name_Column)).Click();
        }

        [Then(@"the payee list should be sorted in alphabetical order by name")]
        public void ThenThePayeeListShouldBeSortedInAlphabeticalOrderByName()
        {
            Pages.Payees.SortNewlyAddedPayeeByName(driverhelper.driver);
        }

        [AfterScenario("Payee")]
        public void Teardown(TestContext testContext)
        {
            if(!testContext.CurrentTestOutcome.ToString().Contains("Passed"))
            {
                ReuseAppActions.TakeScreenshot(driverhelper.driver, testContext.TestName);
            }
            driverhelper.driver.Quit();
        }
    }
}
