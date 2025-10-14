using MARSCOMPETITION.Driver;
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
    public class LoginPage : CommonDriver
    {


        private IWebDriver driver;
        private WebDriverWait wait;

        private readonly By SigninButton = By.XPath("//a[@class='item' and text() ='Sign In']");
        private readonly By Email = By.XPath("//input[@name='email']");
        private readonly By Password = By.XPath("//input[@name='password']");
        private readonly By LoginButton = By.XPath("//button[@class='fluid ui teal button']");
        private readonly By InvalidEmailID = By.XPath("//div[@class='ui basic red pointing prompt label transition visible'][1]");
        private readonly By WrongPwdMessage = By.XPath("//div[@class='ns-box-inner']");
        private readonly By BlankPwdMsg = By.XPath("//input[@name='password']/following-sibling::div[contains(@class,'ui basic red pointing prompt label')]");
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver!, TimeSpan.FromSeconds(10));
            
        }
        public void NavigateToLoginPage()
        {
            driver!.Navigate().GoToUrl("http://localhost:5003/Home");

        }
        public void Login(string email, string password)
        {
            driver!.FindElement(SigninButton).Click();
            driver.FindElement(Email).SendKeys(email);
            driver.FindElement(Password).SendKeys(password);
            wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(LoginButton)).Click();
        }
        public string GetInvalidEmailMessage()
        {
            try
            {
                var invalidEmailElement = wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(InvalidEmailID));
                return invalidEmailElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }
        public string GetWrongPwdMessage()
        {
            try
            {
                var errorElement = wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(WrongPwdMessage));
                return errorElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }

        public string GetBlankPwdMsg()
        {
            try
            {
                var errorElement = wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(BlankPwdMsg));
                return errorElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }
        

    }
}
