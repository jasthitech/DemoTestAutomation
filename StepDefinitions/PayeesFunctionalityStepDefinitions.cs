using DemoTestAutomation.Helpers;
using DemoTestAutomation.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace DemoTestAutomation.StepDefinitions
{
    [Binding]
    public class PayeesFunctionalityStepDefinitions
    {
        [Given(@"I am on the BNZ demo application home page")]
        public void GivenIAmOnTheBNZDemoApplicationHomePage()
        {           
            Helpers.Webdriverhelper.NavigateToPage(Helpers.Webdriverhelper.GetWebDriver(),
                Helpers.Webdriverhelper.GetBaseUrl());
        }

        [When(@"I click on the '([^']*)' option")]
        public void WhenIClickOnTheOption(string menu)
        {
            WebDriver webDriver = Helpers.Webdriverhelper.driver;
            ReuseAppActions.ClickOnMainMenu(webDriver);            
        }

        [When(@"I click on the '([^']*)' option in the navigation menu")]
        public void WhenIClickOnTheOptionInTheNavigationMenu(string payees)
        {
            WebDriver webDriver = Helpers.Webdriverhelper.driver;
            ReuseAppActions.ClickOnMainMenuPayees(webDriver);
        }

        [Then(@"the Payees page should be loaded successfully\.")]
        public void ThenThePayeesPageShouldBeLoadedSuccessfully_()
        {
            WebDriver webDriver = Helpers.Webdriverhelper.driver;
            Payees.VerifyPayeesPageHeading(webDriver);
            Webdriverhelper.Quit();
        }

        [Given(@"I am on the Payees page")]
        public void GivenIAmOnThePayeesPage()
        {
            WebDriver webDriver = Helpers.Webdriverhelper.GetWebDriver();
            Payees.NavigateToPayeesPage(webDriver);
            Payees.VerifyPayeesPageHeading(webDriver);
        }

        [When(@"I click the '([^']*)' button")]
        public void WhenIClickTheButton(string add)
        {
            Pages.Payees.ClickOnPayeeAddBtn(Webdriverhelper.driver);
        }

        [When(@"I enter the payee details \(name, account number\)")]
        public void WhenIEnterThePayeeDetailsNameAccountNumber()
        {
            Pages.Payees.EnterPayeeDetails(Webdriverhelper.driver);
        }

        [When(@"I click the '([^']*)' button on the model")]
        public void WhenIClickTheButtonOnTheModel(string add)
        {
            Pages.Payees.ClickOnPayeeModelAddBtn(Webdriverhelper.driver);
        }

        [Then(@"I should see the message '([^']*)'")]
        public void ThenIShouldSeeTheMessage(string p0)
        {
            Pages.Payees.CheckForPayeeAddedMsg(Webdriverhelper.driver);
        }

        [Then(@"the payee should be added to the list of payees")]
        public void ThenThePayeeShouldBeAddedToTheListOfPayees()
        {
            Pages.Payees.CheckForNewlyAddedPayeeInTheList(Webdriverhelper.driver);
            Webdriverhelper.Quit();
        }

        [Then(@"I should see validation errors")]
        public void ThenIShouldSeeValidationErrors()
        {
            Pages.Payees.isValidationErrorDisplayedOnPayeeMdl(Webdriverhelper.driver);
        }

        [When(@"I populate the mandatory fields")]
        public void WhenIPopulateTheMandatoryFields()
        {
            Pages.Payees.EnterPayeeDetails(Webdriverhelper.driver);
        }

        [Then(@"the validation errors should disappear")]
        public void ThenTheValidationErrorsShouldDisappear()
        {
            Pages.Payees.isValidationErrorNotDisplayedOnPayeeMdl(Webdriverhelper.driver);
            Webdriverhelper.Quit();
        }

        [Given(@"there are existing payees in the list")]
        public void GivenThereAreExistingPayeesInTheList()
        {
            Pages.Payees.VerifyPayeeListHasExistingPayees(Webdriverhelper.driver);
        }

        [When(@"the user adds a new payee")]
        public void WhenTheUserAddsANewPayee()
        {
            Pages.Payees.ClickOnPayeeAddBtn(Webdriverhelper.driver);
            Pages.Payees.EnterPayeeDetails(Webdriverhelper.driver);
            Pages.Payees.ClickOnPayeeModelAddBtn(Webdriverhelper.driver);
            Pages.Payees.CheckForNewlyAddedPayeeInTheList(Webdriverhelper.driver);
        }

        [Then(@"the payee list should be sorted in ascending order by default")]
        public void ThenThePayeeListShouldBeSortedInAscendingOrderByDefault()
        {
            Pages.Payees.VerifyNewlyAddedPayeeIsAtTopOfList(Webdriverhelper.driver);
        }


        [Then(@"the user clicks the Name header")]
        public void ThenTheUserClicksTheNameHeader()
        {
            Webdriverhelper.driver.FindElement(By.XPath(Payees.payees_List_Name_Column)).Click();
        }

        [Then(@"the payee list should be sorted in alphabetical order by name\.")]
        public void ThenThePayeeListShouldBeSortedInAlphabeticalOrderByName_()
        {
            Pages.Payees.SortNewlyAddedPayeeByName(Webdriverhelper.driver);
            Webdriverhelper.Quit();
        }














    }
}
