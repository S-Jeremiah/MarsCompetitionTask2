using MARSCOMPETITION.Driver;
using MARSCOMPETITION.Model;
using MARSCOMPETITION.Tests;
using MARSCOMPETITION.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Pages
{
    public class EducationPage : CommonDriver
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private readonly By educationTab = By.XPath("//a[@data-tab='third']");
        private readonly By addNewButton = By.XPath("//div[@data-tab='third']//div[contains(@class,'ui teal button') and text()='Add New']");
        private readonly By collegeInput = By.XPath("//input[@placeholder='College/University Name']");
        private readonly By countryDropdown = By.XPath("//div[@data-tab='third']//select[@name='country']");
        private readonly By titleDropdown = By.XPath("//div[@data-tab='third']//select[@name='title']");
        private readonly By degreeInput = By.XPath("//input[@placeholder='Degree']");
        private readonly By yearDropdown = By.XPath("//div[@data-tab='third']//select[@name='yearOfGraduation']");
        private readonly By addBtn = By.XPath("//div[@data-tab='third']//input[@type='button' and @value='Add']");
        private readonly By messageBox = By.XPath("//div[@class='ns-box-inner']");
        private readonly By editIcon = By.XPath("//div[@data-tab='third']//i[@class='outline write icon']");
        private readonly By updateBtn = By.XPath("//div[@data-tab='third']//input[@type='button' and @value='Update']");
        public By educationRows = By.XPath("//div[@data-tab='third']//table/tbody/tr");

        public EducationPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver!, TimeSpan.FromSeconds(10));

        }
        public void OpenEducationTab()
        {
            wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(educationTab)).Click();
        }
        public void ClickAddNew()

        {
            OpenEducationTab();
            wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addNewButton)).Click();
        }
        public void EnterCollege(string college)
        {
            if(driver == null)
            throw new InvalidOperationException("WebDriver is not initialized. Did you call Initialise()?");
            var element = driver.FindElement(collegeInput);
            element.Clear();
            if (!string.IsNullOrWhiteSpace(college))
                element.SendKeys(college);
        }
        public void SelectCountry(string country)
        {
            if (driver == null)
                throw new InvalidOperationException("WebDriver is not initialized. Did you call Initialise()?");
            var dropdown = new SelectElement(driver.FindElement(countryDropdown));
            if (!string.IsNullOrWhiteSpace(country))
                dropdown.SelectByValue(country.Trim());
        }
        public void SelectTitle(string title)
        {
            if (driver == null)
                throw new InvalidOperationException("WebDriver is not initialized. Did you call Initialise()?");
            var dropdown = new SelectElement(driver.FindElement(titleDropdown));
            if (!string.IsNullOrWhiteSpace(title))
                dropdown.SelectByValue(title.Trim());
        }
        public void EnterDegree(string degree)
        {
            if (driver == null)
                throw new InvalidOperationException("WebDriver is not initialized. Did you call Initialise()?");
            var element = driver.FindElement(degreeInput);
            element.Clear();
            if (!string.IsNullOrWhiteSpace(degree))
                element.SendKeys(degree);
        }
        public void SelectYear(string year)
        {
            if (driver == null)
                throw new InvalidOperationException("WebDriver is not initialized. Did you call Initialise()?");
            var dropdown = new SelectElement(driver.FindElement(yearDropdown));
            if (!string.IsNullOrWhiteSpace(year))
                dropdown.SelectByValue(year.Trim());
        }
        public void SaveEducation()
        {
            wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addBtn)).Click();

        }
        public void AddEducation(AddEducationData education)
        {
            ClickAddNew();
            EnterCollege(education.College);
            SelectCountry(education.Country);
            SelectTitle(education.Title);
            EnterDegree(education.Degree);
            SelectYear(education.Year);
            SaveEducation();
            wait!.Until(d => GetPopupMessage().Contains("Education has been added"));
        }



        public void EditEducationSimple(EditEducationData education)
        {
            OpenEducationTab();

            foreach (var row in driver!.FindElements(educationRows))
            {
                string collegeName = row.FindElement(By.XPath("./td[2]")).Text.Trim();
                string countryName = row.FindElement(By.XPath("./td[1]")).Text.Trim();

                if (collegeName.Equals(education.ExistingCollege, StringComparison.OrdinalIgnoreCase) &&
                    countryName.Equals(education.ExistingCountry, StringComparison.OrdinalIgnoreCase))
                {
                    row.FindElement(By.XPath(".//i[contains(@class,'outline write icon')]")).Click();

                    if (!string.IsNullOrWhiteSpace(education.NewCollege))
                    {
                        var collegeInputElement = driver.FindElement(collegeInput);
                        collegeInputElement.Clear();
                        collegeInputElement.SendKeys(education.NewCollege);
                    }

                    if (!string.IsNullOrWhiteSpace(education.NewCountry))
                    {
                        var countrySelect = new SelectElement(driver.FindElement(countryDropdown));
                        countrySelect.SelectByText(education.NewCountry);
                    }
                    if (!string.IsNullOrWhiteSpace(education.NewTitle))
                    {
                        var countrySelect = new SelectElement(driver.FindElement(titleDropdown));
                        countrySelect.SelectByValue(education.NewTitle);
                    }

                    driver.FindElement(By.XPath("//input[@type='button' and @value='Update']")).Click();
                    wait!.Until(d => GetPopupMessage().Contains("Education as been updated"));
                    return;
                }
            }

            throw new Exception($"Row with College='{education.ExistingCollege}' and Country='{education.ExistingCountry}' not found.");
        }



        public string GetPopupMessage()
        {
            try
            {
                var errorElement = wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(messageBox));
                return errorElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty; // No error message found within the timeout
            }
        }

        public void DeleteEducation(DeleteEducation education)
        {
            OpenEducationTab();
            foreach (var row in driver!.FindElements(educationRows))
            {
                string collegeName = row.FindElement(By.XPath("./td[2]")).Text.Trim();
                string countryName = row.FindElement(By.XPath("./td[1]")).Text.Trim();
                string titleName = row.FindElement(By.XPath("./td[3]")).Text.Trim();
                string degreeName = row.FindElement(By.XPath("./td[4]")).Text.Trim();
                string yearName = row.FindElement(By.XPath("./td[5]")).Text.Trim();
                if (collegeName.Equals(education.College.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    countryName.Equals(education.Country.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    titleName.Equals(education.Title.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    degreeName.Equals(education.Degree.Trim(), StringComparison.OrdinalIgnoreCase) &&
                    yearName.Equals(education.Year.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    row.FindElement(By.XPath(".//i[contains(@class,'remove icon')]")).Click();
                    
                    wait!.Until(d => GetPopupMessage().Contains("Education entry successfully removed"));
                    return;
                }
            }
            throw new Exception($"Row with College='{education.College}' not found.");

        }
    }
}

