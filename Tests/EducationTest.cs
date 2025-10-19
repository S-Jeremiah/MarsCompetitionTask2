using MARSCOMPETITION.Model;
using MARSCOMPETITION.Pages;
using MARSCOMPETITION.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MARSCOMPETITION.Model.AddEducationData;
using static MARSCOMPETITION.Model.DeleteEducation;

namespace MARSCOMPETITION.Tests
{
    public class EducationTest : BaseTest
    {
        [Test]
        [TestCaseSource(nameof(GetAddEducationData))]
        public void AddEducationTest(AddEducationData education)
        {
            var educationPage = new EducationPage(driver);
            educationPage.OpenEducationTab();
            educationPage.ClickAddNew();
            educationPage.EnterCollege(education.College);
            educationPage.SelectCountry(education.Country);
            educationPage.SelectTitle(education.Title);
            educationPage.EnterDegree(education.Degree);
            educationPage.SelectYear(education.Year);
            educationPage.SaveEducation();
            BaseTest.TrackAddedEducation(education.College);

            // Validate
            string actualMsg = educationPage.GetPopupMessage();

            switch (education.ExpectedResult)
            {
                case "Success":
                    Assert.That(actualMsg, Is.EqualTo("Education has been added"),
                        $"Scenario: {education.Scenario}. Expected success but got: {actualMsg}");
                    break;

                case "BlankCountry":
                case "BlankCollege":
                case "BlankTitle":
                case "BlankYear":
                case "BlankDegree":
                    Assert.That(actualMsg, Is.EqualTo("Please enter all the fields"),
                        $"Scenario: {education.Scenario}. Expected 'Please enter all the fields' but got: {actualMsg}");
                    break;

                case "Information already Exist":
                    Assert.That(actualMsg, Is.EqualTo("This information is already exist."),
                        $"Scenario: {education.Scenario}. Expected duplicate warning but got: {actualMsg}");
                    break;

                default:
                    Assert.Fail($"Unknown ExpectedResult: {education.ExpectedResult} in scenario: {education.Scenario}");
                    break;
            }



        }


        [Test]
        [TestCaseSource(nameof(GetEditEducationData))]
        public void EditEducationTest(EditEducationData education)
        {
            var educationPage = new EducationPage(driver);
            educationPage.AddEducation(new AddEducationData
            {
                College = education.ExistingCollege,
                Country = education.ExistingCountry,
                Title = education.ExistingTitle,
                Degree = education.Degree,
                Year = education.Year,
                ExpectedResult = "Success"
            });
            BaseTest.TrackAddedEducation(education.ExistingCollege);
            BaseTest.TrackAddedEducation(education.ExistingCountry);
            BaseTest.TrackAddedEducation(education.ExistingTitle);
            Thread.Sleep(500); // small wait for UI

            // Edit the record
            educationPage.EditEducationSimple(education);
            string actualMsg = educationPage.GetPopupMessage();

            Assert.That(actualMsg, Is.EqualTo("Education as been updated"),
                $"Expected 'Education has been updated' but got: {actualMsg}");
            BaseTest.TrackAddedEducation(education.NewCollege);
            BaseTest.TrackAddedEducation(education.NewCountry);
            BaseTest.TrackAddedEducation(education.NewTitle);
        }

        [Test]
        [TestCaseSource(nameof(GetDeleteEducationData))]

        public void DeleteEducationTest(DeleteEducation education)
        {
            var educationPage = new EducationPage(driver);
            educationPage.AddEducation(new AddEducationData
            {
                College = education.College.Trim(),
                Country = education.Country.Trim(),
                Title = education.Title.Trim(),
                Degree = education.Degree.Trim(),
                Year = education.Year.Trim(),
                ExpectedResult = "Success"
            });
            BaseTest.TrackAddedEducation(education.College);
            BaseTest.TrackAddedEducation(education.Country);

            Thread.Sleep(500); // small wait for UI
            // Delete the record
            educationPage.DeleteEducation(education);
            string actualMsg = educationPage.GetPopupMessage();
            Assert.That(actualMsg, Is.EqualTo("Education entry successfully removed"),
                $"Expected 'Education entry successfully removed' but got: {actualMsg}");


        }


        public static IEnumerable<AddEducationData> GetAddEducationData()
        {
            string jsonDataAdd = File.ReadAllText("TestData/AddEducation.json");
            var adddata = JsonConvert.DeserializeObject<EducationDataSet>(jsonDataAdd);
            if (adddata == null || adddata.Educations == null)
                throw new InvalidOperationException("AddEducation.json could not be deserialized properly.");
            return adddata.Educations;
        }
        public static IEnumerable<EditEducationData> GetEditEducationData()
        {
            // string  jsonData =File.ReadAllText("TestData/EditEducationData.json");
            var editdata = Utilities.JsonReader.LoadJson<EditEducationDataSet>("TestData/EditEducationData.json");
            return editdata.Educations;
        }
        public static IEnumerable<DeleteEducation> GetDeleteEducationData()
        {
            string jsonData = File.ReadAllText("TestData/DeleteEducation.json");
            var deletedata = JsonConvert.DeserializeObject<DeleteEducationDataSet>(jsonData);
            if (deletedata == null || deletedata.Educations == null)
                throw new InvalidOperationException("AddEducation.json could not be deserialized properly.");
            return deletedata.Educations;
        }
    }
}
