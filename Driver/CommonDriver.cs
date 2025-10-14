using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace MARSCOMPETITION.Driver
{
    public class CommonDriver
    {
        protected IWebDriver? driver;
        protected WebDriverWait? wait;

        public void Initialise()
        {
            if (driver == null)
            {
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                var options = new ChromeOptions();
                options.AddUserProfilePreference("profile.password_manager_leak_detection", false);


                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();

                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            }
        }

        public void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }


    }

}

