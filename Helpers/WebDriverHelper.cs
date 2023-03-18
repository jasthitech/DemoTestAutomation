namespace DemoTestAutomation.Helpers
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;
    using SpecFlow.Internal.Json;

    public class Webdriverhelper
    {
        public string baseUrl = "";
        public string browserType;
        public string chromeDriverFullPath;        
        public string geckoDriverFullPath;
        public WebDriver driver;
        public WebDriverWait wait;
        // Set the path to the Firefox binary
        public string firefoxBinaryPath;
        public Webdriverhelper()
        {
            baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            browserType = ConfigurationManager.AppSettings["Browser"];
            string chrmDriverPathSysPath = ConfigurationManager.AppSettings["ChrmDriverPathForSysPath"];
            string geckoDriverPathSysPath = ConfigurationManager.AppSettings["geckoDriverPathToSetSysPath"];            
            firefoxBinaryPath = ConfigurationManager.AppSettings["firefoxBinaryPath"];

            // Get the path of the project folder
            string projectFolderPath = Directory.GetCurrentDirectory();

            // Combine the project folder path with chromedriver.exe filename to get the full path
            chromeDriverFullPath = Path.Combine(projectFolderPath, chrmDriverPathSysPath);

            // Combine the project folder path with geckdriver.exe filename to get the full path
            geckoDriverFullPath = Path.Combine(projectFolderPath, geckoDriverPathSysPath);           

        }

        public WebDriver GetWebDriver()
        {
            // Generate a random string using the Guid class
            string randomString_myProfile = Guid.NewGuid().ToString();
            switch (browserType)
            {
                case "Chrome":
                    var options = new ChromeOptions();
                    // Set the path to the Chrome Browser executable
                    options.BinaryLocation = ConfigurationManager.AppSettings["chromeBinaryPath"];                    
                    string chromeProfilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\") + randomString_myProfile;

                    // Substitute the user name in the profile path
                    string userName = Environment.UserName;
                    chromeProfilePath = Path.Combine(chromeProfilePath, userName);                    
                    string finalProfilePath = "user-data-dir=" + chromeProfilePath;
                    options.AddArgument(finalProfilePath);
                    options.AddArguments("--start-maximized"); // Add any other desired Chrome options
                    var service = ChromeDriverService.CreateDefaultService(chromeDriverFullPath); // Set the path to the ChromeDriver executable
                    driver = new ChromeDriver(service, options);
                    break;
                case "Firefox":                    
                    FirefoxProfileManager profileManager = new FirefoxProfileManager();
                    FirefoxProfile fxProfile = profileManager.GetProfile("default");

                    // Set the path to the Firefox profile directory
                    string firefoxProfilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Mozilla\Firefox\Profiles", randomString_myProfile);
                    Directory.CreateDirectory(firefoxProfilePath);
                    fxProfile = new FirefoxProfile(firefoxProfilePath);

                    // Set the path to the Firefox Browser executable
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.BrowserExecutableLocation = firefoxBinaryPath;
                    firefoxOptions.Profile = fxProfile;
                    
                    // Create the Firefox driver
                    driver = new FirefoxDriver(geckoDriverFullPath, firefoxOptions);
                    break;                
                // Add other cases for other browsers if needed
                default:
                    throw new ArgumentException("Invalid browser type specified in Config.xml");
            }
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            // Set up a WebDriverWait object with a timeout of 10 seconds
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            return driver;
        }

        public string GetBaseUrl()
        {
            return baseUrl;
        }

        public void RetryCheckElementNotPresent(WebDriver webDriver, string web_element)
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

        public void WaitForPageLoad(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void NavigateToPage(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitForPageLoad(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }
    }

}
