using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestAutomation.Helpers
{
    public static class ReuseAppActions
    {
        public static void ClickOnMainMenu(WebDriver webDriver)
        {            
            Webdriverhelper.RetryClick(webDriver, Pages.Home.mainMenu);
        }

        public static void ClickOnMainMenuPayees(WebDriver webDriver)
        {           
            Webdriverhelper.RetryClick(webDriver, Pages.Home.mainMenuPayees);
        }

        public static void ClickOnMainMenuPayOrTransfer(WebDriver webDriver)
        {            
            Webdriverhelper.RetryClick(webDriver, Pages.Home.mainMenuPayOrTransfer);
        }
    }
}
