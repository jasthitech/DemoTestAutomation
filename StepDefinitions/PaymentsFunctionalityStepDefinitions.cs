using DemoTestAutomation.Helpers;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoTestAutomation.StepDefinitions
{
    [Binding]
    public class PaymentsFunctionalityStepDefinitions
    {
        private readonly TestContext _testContext;
        public Webdriverhelper driverhelper;
        public PaymentsFunctionalityStepDefinitions(TestContext testContext) // use it as ctor parameter
        {
            _testContext = testContext;
            driverhelper = new Webdriverhelper();
        }

        [Given(@"a user is on the Payments page")]
        public void GivenAUserIsOnThePaymentsPage()
        {
            WebDriver webDriver = driverhelper.GetWebDriver();
            driverhelper.NavigateToPage(webDriver,
                driverhelper.GetBaseUrl());
            ReuseAppActions.ClickOnMainMenu(webDriver, driverhelper.wait);
            ReuseAppActions.ClickOnMainMenuPayOrTransfer(webDriver, driverhelper.wait);
            Pages.Payments.VerifyPaymentsPanelDisplayed(webDriver);
        }

        [Given(@"the Everyday account has a balance greater than \$(.*)")]
        public void GivenTheEverydayAccountHasABalanceGreaterThan(int amount)
        {
            Pages.Payments.VerifyPaymentsPanelDisplayed(driverhelper.driver);
            Pages.Payments.ClickOnChooseAccount(driverhelper.driver, driverhelper.wait);
            Pages.Payments.ValidateEverydayAccBalance(driverhelper.driver, amount);
        }

        [Given(@"the Bills account has a balance less than \$(.*)")]
        public void GivenTheBillsAccountHasABalanceLessThan(int amount)
        {
            Pages.Payments.VerifyBillsAccountBalance(driverhelper.driver, amount);
        }

        [When(@"the user transfers \$(.*) from the Everyday account to the Bills account")]
        public void WhenTheUserTransfersFromTheEverydayAccountToTheBillsAccount(int billAmount)
        {
            Pages.Payments.SelectEverydayAccountAsFromAcc(driverhelper.driver, billAmount, driverhelper.wait);
            Pages.Payments.ClickOnChooseToAccount(driverhelper.driver, driverhelper.wait);
            Pages.Payments.ClickOnAccountsTab(driverhelper.driver);
            Pages.Payments.ChooseBillAccAsToAcc(driverhelper.driver, driverhelper.wait);
            Pages.Payments.SetFocusAndEnterAmount(driverhelper.driver, billAmount);
            Pages.Payments.ClickOnTransferBtn(driverhelper.driver);
        }

        [Then(@"a transfer successful message should be displayed")]
        public void ThenATransferSuccessfulMessageShouldBeDisplayed()
        {
            Pages.Payments.VerifyTransferSuccessMsg(driverhelper.driver, driverhelper.wait);
        }

        [Then(@"the current balance of the Everyday account should be reduced by \$(.*)")]
        public void ThenTheCurrentBalanceOfTheEverydayAccountShouldBeReducedBy(int reducedAmount)
        {
            ReuseAppActions.ClickOnMainMenu(driverhelper.driver, driverhelper.wait);
            ReuseAppActions.ClickOnMainMenuPayOrTransfer(driverhelper.driver, driverhelper.wait);
            Pages.Payments.VerifyPaymentsPanelDisplayed(driverhelper.driver);
            Pages.Payments.ClickOnChooseAccount(driverhelper.driver, driverhelper.wait);
            Pages.Payments.VerifyBalanceOfEverydayAccountAfterTransfer(driverhelper.driver, reducedAmount);
        }

        [Then(@"the current balance of the Bills account should be increased by \$(.*)")]
        public void ThenTheCurrentBalanceOfTheBillsAccountShouldBeIncreasedBy(int increasedAmount)
        {
            Pages.Payments.VerifyBalanceOfBillsAccountAfterTransfer(driverhelper.driver, increasedAmount);
        }

        [AfterScenario("Payments")]
        public void Teardown(TestContext testContext)
        {
            driverhelper.driver.Quit();
        }
    }
}
