using MARSCOMPETITION.Driver;
using MARSCOMPETITION.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Pages
{
    public class CertificationPage : CommonDriver
    {

        private IWebDriver driver;
        private WebDriverWait wait;

        private readonly By certificationTab = By.XPath("//a[@data-tab='fourth']");
        private readonly By addNewButton = By.XPath("//div[@data-tab='fourth']//div[contains(@class,'ui teal button') and text()='Add New']");
        private readonly By certificateInput = By.XPath("//input[@placeholder='Certificate or Award']");
        private readonly By certifiedFromInput = By.XPath("//input[@placeholder='Certified From (e.g. Adobe)']");
        private readonly By yearDropdown = By.XPath("//div[@data-tab='fourth']//select[@name='certificationYear']");
        public By CertificationRows = By.XPath("//div[@data-tab='fourth']//table/tbody/tr");
        private readonly By addBtn = By.XPath("//div[@data-tab='fourth']//input[@type='button' and @value='Add']");
        private readonly By cancelBtn = By.XPath("//div[@data-tab='fourth']//input[@type='button' and @value='Cancel']");
        private readonly By messageBox = By.XPath("//div[@class='ns-box-inner']");
        private readonly By editIcon = By.XPath("//div[@data-tab='fourth']//i[@class='outline write icon']");
        private readonly By updateBtn = By.XPath("//div[@data-tab='fourth']//input[@type='button' and @value='Update']");

        public CertificationPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver!, TimeSpan.FromSeconds(10));
        }

        public void OpenCertificationTab()
        {
            wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(certificationTab)).Click();
        }
        public void ClickAddNew()
        {
            OpenCertificationTab();
            wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addNewButton)).Click();
        }
        public void EnterCertificate(string certificate)
        {
            if (driver == null)
                throw new InvalidOperationException("WebDriver is not initialized. Did you call Initialise()?");
            var element = driver.FindElement(certificateInput);
            element.Clear();
            if (!string.IsNullOrWhiteSpace(certificate))
                element.SendKeys(certificate);
        }
        public void EnterCertifiedFrom(string certifiedFrom)
        {
            if (driver == null)
                throw new InvalidOperationException("WebDriver is not initialized. Did you call Initialise()?");
            var element = driver.FindElement(certifiedFromInput);
            element.Clear();
            if (!string.IsNullOrWhiteSpace(certifiedFrom))
                element.SendKeys(certifiedFrom);
        }
        public void SelectYear(string year)
        {
            if (driver == null)
                throw new InvalidOperationException("WebDriver is not initialized. Did you call Initialise()?");
            var yearElement = driver.FindElement(yearDropdown);
            var selectYear = new SelectElement(yearElement);
            if (!string.IsNullOrWhiteSpace(year))
                selectYear.SelectByValue(year);
        }
        public void ClickAddButton()
        {
            wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addBtn)).Click();
        }
        public void ClickCancelButton()
        {
            wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(cancelBtn)).Click();
        }
        public string GetMessage()
        {
            try
            {
                var messageElement = wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(messageBox));
                return messageElement.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;
            }
        }

        public void AddCertification(AddCertificationData certification)
        {
            ClickAddNew();
            EnterCertificate(certification.Certificate);
            EnterCertifiedFrom(certification.CertifiedFrom);
            SelectYear(certification.Year);
            ClickAddButton();
            wait!.Until(d => GetMessage().Contains(certification.Certificate + " has been added"));
        }

        public void EditCertification(Editcertification certification)
        {
            OpenCertificationTab();
            foreach (var row in driver!.FindElements(CertificationRows))
            {
                string certificationName = row.FindElement(By.XPath(".//td[1]")).Text;
                string CertifiedfromName = row.FindElement(By.XPath(".//td[2]")).Text;


                if (certificationName.Equals(certification.ExistingCertificate, StringComparison.OrdinalIgnoreCase) &&
                    CertifiedfromName.Equals(certification.ExistingCertifiedFrom, StringComparison.OrdinalIgnoreCase))
                {
                    row.FindElement(By.XPath(".//i[contains(@class,'outline write icon')]")).Click();
                    EnterCertificate(certification.NewCertificate);
                    EnterCertifiedFrom(certification.NewCertifiedFrom);
                    SelectYear(certification.NewYear);
                    wait!.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(updateBtn)).Click();
                    wait!.Until(d => GetMessage().Contains(certification.NewCertificate + " has been updated"));
                    break;
                }


            }
        }
        public void DeleteCertification(DeleteCertification certification)
        {
            OpenCertificationTab();
            foreach (var row in driver!.FindElements(CertificationRows))
            {
                string certificationName = row.FindElement(By.XPath(".//td[1]")).Text;
                string certifiedfromName = row.FindElement(By.XPath(".//td[2]")).Text;
                string year = row.FindElement(By.XPath(".//td[3]")).Text;
                if (certificationName.Equals(certification.Certificate, StringComparison.OrdinalIgnoreCase) &&
                    certifiedfromName.Equals(certification.CertifiedFrom, StringComparison.OrdinalIgnoreCase) &&
                    year.Equals(certification.Year, StringComparison.OrdinalIgnoreCase))
                {
                    row.FindElement(By.XPath(".//i[contains(@class,'remove icon')]")).Click();
                    wait!.Until(d => GetMessage().Contains(certification.Certificate + " has been deleted from your certification"));
                    break;
                }
            }
        }
    }
}
