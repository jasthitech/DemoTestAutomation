using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestAutomation.Pages
{
    public static class Home
    {
        public static string mainMenu = "//button[@class='Button Button--transparent js-main-menu-btn MenuButton']";
        public static string mainMenuPayees = "//a[@href='/client/payees']";
        public static string mainMenuPayOrTransfer = "//span[text()='Pay or transfer']";
    }
}
