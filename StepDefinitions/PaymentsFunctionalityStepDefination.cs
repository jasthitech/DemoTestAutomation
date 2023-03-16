using DemoTestAutomation.Helpers;
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
    public class PaymentsFunctionalityStepDefination
    {
        [Given(@"a user is on the Payments page")]
        public void GivenAUserIsOnThePaymentsPage()
        {
            WebDriver webDriver = Helpers.Webdriverhelper.GetWebDriver();
            Helpers.Webdriverhelper.NavigateToPage(webDriver,
                Helpers.Webdriverhelper.GetBaseUrl());
            ReuseAppActions.ClickOnMainMenu(webDriver);
            ReuseAppActions.ClickOnMainMenuPayOrTransfer(webDriver);
            Pages.Payments.VerifyPaymentsPanelDisplayed(webDriver);
        }

        [Given(@"the Everyday account has a balance greater than \$(.*)")]
        public void GivenTheEverydayAccountHasABalanceGreaterThan(int amount)
        {
            Pages.Payments.VerifyPaymentsPanelDisplayed(Webdriverhelper.driver);
            Pages.Payments.ClickOnChooseAccount(Webdriverhelper.driver);
            Pages.Payments.ValidateEverydayAccBalance(Webdriverhelper.driver, amount);
        }

        [Given(@"the Everyday account has a balance greater than five hundred")]
        public void GivenTheEverydayAccountHasABalanceGreaterThanFiveHundred()
        {
            Pages.Payments.VerifyPaymentsPanelDisplayed(Webdriverhelper.driver);
            Pages.Payments.ClickOnChooseAccount(Webdriverhelper.driver);         
        }

        [Given(@"the Bills account has a balance less than \$(.*)")]
        public void GivenTheBillsAccountHasABalanceLessThan(int amount)
        {
            Pages.Payments.VerifyBillsAccountBalance(Webdriverhelper.driver, amount);
        }

        [When(@"the user transfers \$(.*) from the Everyday account to the Bills account")]
        public void WhenTheUserTransfersFromTheEverydayAccountToTheBillsAccount(int billAmount)
        {
            Pages.Payments.SelectEverydayAccountAsFromAcc(Webdriverhelper.driver, billAmount);
            Pages.Payments.ClickOnChooseToAccount(Webdriverhelper.driver);
            Pages.Payments.ClickOnAccountsTab(Webdriverhelper.driver);
            Pages.Payments.ChooseBillAccAsToAcc(Webdriverhelper.driver);
            Pages.Payments.SetFocusAndEnterAmount(Webdriverhelper.driver, billAmount);
            Pages.Payments.ClickOnTransferBtn(Webdriverhelper.driver);
        }

        [Then(@"a transfer successful message should be displayed")]
        public void ThenATransferSuccessfulMessageShouldBeDisplayed()
        {
            Pages.Payments.VerifyTransferSuccessMsg(Webdriverhelper.driver);
        }

        [Then(@"the current balance of the Everyday account should be reduced by \$(.*)")]
        public void ThenTheCurrentBalanceOfTheEverydayAccountShouldBeReducedBy(int reducedAmount)
        {
            ReuseAppActions.ClickOnMainMenu(Webdriverhelper.driver);
            ReuseAppActions.ClickOnMainMenuPayOrTransfer(Webdriverhelper.driver);
            Pages.Payments.VerifyPaymentsPanelDisplayed(Webdriverhelper.driver);
            Pages.Payments.ClickOnChooseAccount(Webdriverhelper.driver);
            Pages.Payments.VerifyBalanceOfEverydayAccountAfterTransfer(Webdriverhelper.driver, reducedAmount);
        }

        [Then(@"the current balance of the Bills account should be increased by \$(.*)")]
        public void ThenTheCurrentBalanceOfTheBillsAccountShouldBeIncreasedBy(int increasedAmount)
        {
            Pages.Payments.VerifyBalanceOfBillsAccountAfterTransfer(Webdriverhelper.driver, increasedAmount);
            Webdriverhelper.Quit();
        }








    }
}
