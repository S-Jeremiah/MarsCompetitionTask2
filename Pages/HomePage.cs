using MARSCOMPETITION.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Pages
{
    public class HomePage
    {

        private readonly IWebDriver Driver;
        private readonly WebDriverWait wait;
        private By loggedInUserName = By.XPath("//div[@class='ui compact menu']//span[contains(@class,'dropdown') and contains(@class,'link')][last()]");

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            
        }

        
        public string GetLoggedInUserName()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(40));


            IWebElement userElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(loggedInUserName));
            return userElement.Text;  // Returns the displayed username
        }

    }
}
