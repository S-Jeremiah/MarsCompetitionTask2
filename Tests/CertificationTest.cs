using MARSCOMPETITION.Model;
using MARSCOMPETITION.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MARSCOMPETITION.Model.AddCertificationData;
using static MARSCOMPETITION.Model.DeleteCertification;

namespace MARSCOMPETITION.Tests
{
    public class CertificationTest :BaseTest
    {
        [Test]
        [TestCaseSource(nameof(GetAddCertificationData))]
        public void AddCertificationTest(AddCertificationData certification) 
        {
            var certificationPage = new CertificationPage(driver);
            certificationPage.OpenCertificationTab();
            certificationPage.ClickAddNew();
            certificationPage.EnterCertificate(certification.Certificate);
            certificationPage.EnterCertifiedFrom(certification.CertifiedFrom);
            certificationPage.SelectYear(certification.Year);
            certificationPage.ClickAddButton();
            BaseTest.TrackAddedCertification(certification.Certificate);
            string actualMsg = certificationPage.GetMessage();
            switch (certification.ExpectedResult)
            {
                case "Success":
                    Assert.That(actualMsg, Is.EqualTo(certification.Certificate + " has been added to your certification"),
                        $"Scenario: {certification.Scenario}. Expected success but got: {actualMsg}");
                    break;

                case "BlankCertificate":
                case "BlankCertifiedfrom":
                case "BlankYear":
                    Assert.That(actualMsg, Is.EqualTo("Please enter Certification Name, Certification From and Certification Year"),
                        $"Scenario: {certification.Scenario}. Expected 'Please enter Certification Name, Certification From and Certification Year' but got: {actualMsg}");
                    break;

                case "Information already Exist":
                    Assert.That(actualMsg, Is.EqualTo("This information is already exist."),
                        $"Scenario: {certification.Scenario}. Expected duplicate warning but got: {actualMsg}");
                    break;

                default:
                    Assert.Fail($"Unknown ExpectedResult: {certification.ExpectedResult} in scenario: {certification.Scenario}");
                    break;
            }
            BaseTest.TrackAddedCertification(certification.Certificate);
            
                

        }
        [Test]
        [TestCaseSource(nameof(GetEditCertificationData))]

        public void EditCertificationTest(Editcertification certification)
        {
            var certificationPage = new CertificationPage(driver);
            certificationPage.OpenCertificationTab();
            certificationPage.AddCertification(new AddCertificationData
            {
                Certificate = certification.ExistingCertificate,
                CertifiedFrom = certification.ExistingCertifiedFrom,
                Year = certification.ExistingYear,
                ExpectedResult = "Success",

            });
            Thread.Sleep(500);
            certificationPage.EditCertification(certification);
            string actualMsg = certificationPage.GetMessage();
            Assert.That(actualMsg, Is.EqualTo(certification.NewCertificate + " has been updated to your certification"),
                $"Scenario: {certification.Scenario}. Expected success but got: {actualMsg}");
            BaseTest.TrackAddedCertification(certification.NewCertificate);
        }
        [Test]
        [TestCaseSource(nameof(GetDeleteCertificationData))]
        public void DeleteCertificationTest(DeleteCertification certification)
        {
            var certificationPage = new CertificationPage(driver);
            certificationPage.OpenCertificationTab();
            certificationPage.AddCertification(new AddCertificationData
            {
                Certificate = certification.Certificate,
                CertifiedFrom =certification.CertifiedFrom,
                Year = certification.Year,
                ExpectedResult = "Success",
            });
            Thread.Sleep(500);

            certificationPage.DeleteCertification(certification);
            string actualMsg = certificationPage.GetMessage();  
            Assert.That(actualMsg, Is.EqualTo(certification.Certificate + " has been deleted from your certification"),
                $"Scenario: {certification.Scenario}. Expected success but got: {actualMsg}");

        }

       
        public static IEnumerable<AddCertificationData> GetAddCertificationData()
        {
            string jsonDataAdd = File.ReadAllText("TestData/AddCertification.json");
            var adddata = JsonConvert.DeserializeObject<CertificationDataSet>(jsonDataAdd);
            if (adddata == null || adddata.Certifications == null)
                throw new InvalidOperationException("AddCertification.json could not be deserialized properly.");
            return adddata.Certifications;
        }
        public static IEnumerable<Editcertification> GetEditCertificationData()
        {
            string jsonDataEdit = File.ReadAllText("TestData/EditCertification.json");
            var editdata = JsonConvert.DeserializeObject<EditCertificationDataSet>(jsonDataEdit);
            if (editdata == null || editdata.Certifications == null)
                throw new InvalidOperationException("EditCertification.json could not be deserialized properly.");
            return editdata.Certifications;
        }
        public static IEnumerable<DeleteCertification> GetDeleteCertificationData()
        {
            string jsonDataDelete = File.ReadAllText("TestData/DeleteCertification.json");
            var deletedata = JsonConvert.DeserializeObject<DeleteCertificationDataSet>(jsonDataDelete);
            if (deletedata == null || deletedata.Certifications == null)
                throw new InvalidOperationException("DeleteCertification.json could not be deserialized properly.");
            return deletedata.Certifications;


        }

    }
}
