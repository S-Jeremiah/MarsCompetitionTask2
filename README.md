# Mars Competition — Selenium C# NUnit Automation Framework
## (Login, Education & Certification Modules)

## Overview
A structured and scalable automated test suite for the Mars web portal,
built using Selenium WebDriver with C# and NUnit. The framework covers
Login, Education, and Certification modules with a mature architecture
that includes data-driven testing via JSON, ExtentReports HTML reporting,
screenshot capture on failure, and a strongly-typed Model layer.

## Tech Stack
| Tool | Purpose |
|------|---------|
| C# | Programming language |
| Selenium WebDriver | Browser automation |
| NUnit | Test runner and assertions |
| ExtentReports | HTML test reporting |
| JSON | External test data format |
| NuGet | Package management |
| Page Object Model | Design pattern |
| Visual Studio | IDE |

## Folder Structure
MARSCOMPETITION/

│

├── Driver/                     → WebDriver setup and browser management

│

├── Model/                      → Strongly-typed C# data model classes

│   ├── AddEducationData.cs     → Model for adding education records

│   ├── EditEducationData.cs    → Model for editing education records

│   ├── DeleteEducation.cs      → Model for deleting education records

│   └── LoginTestData.cs        → Model for login credentials

│

├── Pages/                      → Page Object classes

│   ├── LoginPage.cs

│   ├── HomePage.cs

│   ├── EducationPage.cs

│   └── CertificationPage.cs

│

├── Report/

│   └── report.html             → Generated ExtentReports HTML report

│

├── TestData/                   → Externalised JSON test data files

│   ├── LoginData.json

│   ├── AddEducation.json

│   ├── EditEducationData.json

│   └── DeleteEducation.json

│

├── Tests/                      → NUnit test classes

│   ├── BaseTest.cs             → Shared setup and teardown

│   ├── LoginTest.cs

│   ├── HomeTest.cs

│   ├── EducationTest.cs

│   └── CertificationTest.cs

│

├── Utilities/                  → Reusable helper classes

│   ├── ExtentManager.cs        → ExtentReports instance management

│   ├── ExtentReportSetup.cs    → Report initialisation and configuration

│   ├── JsonReader.cs           → Reads and deserialises JSON test data

│   ├── ScreenshotHelper.cs     → Captures screenshots on test failure

│   └── Wait.cs                 → Custom explicit and implicit wait methods

│

├── MARSCOMPETITION.sln

└── MARSCOMPETITION.csproj

## Test Coverage
| Module | Test Scenarios |
|--------|---------------|
| Login | Valid login, invalid credentials, empty fields |
| Homepage | Navigation and UI validation after login |
| Education | Add, edit, delete education record, |
|           | empty field validation, duplicate entry, |
|           | invalid date handling, cancel action |
| Certification | Add, edit, delete certification, |
|               | empty field validation, duplicate entry, |
|               | invalid year handling, cancel action |

## Key Framework Features

### Data-Driven Testing via JSON
Test data is fully externalised into JSON files in the `TestData/`
folder. The `JsonReader.cs` utility deserialises JSON into strongly-typed
C# model objects at runtime — meaning test data can be updated without
touching any test code.

```json
// Example: AddEducation.json
{
  "University": "Anna University",
  "Country": "India",
  "Title": "Bachelor of Engineering",
  "Degree": "Bachelor",
  "YearOfGraduation": "2015"
}
```

### Strongly-Typed Model Layer
Each test operation (Add, Edit, Delete) has its own C# model class
in the `Model/` folder. This ensures type safety, clear data contracts
between layers, and makes the framework easier to maintain and extend.

### ExtentReports HTML Reporting
`ExtentManager.cs` and `ExtentReportSetup.cs` configure and manage
ExtentReports, generating a rich HTML report (`Report/report.html`)
after each test run. The report includes:
- Pass / Fail / Skip status per test
- Step-by-step test execution logs
- Screenshots attached on failure

### Screenshot on Failure
`ScreenshotHelper.cs` automatically captures and attaches a screenshot
to the ExtentReport when a test fails — making defect investigation
faster and more accurate.

### Custom Wait Utility
`Wait.cs` provides reusable explicit wait methods, avoiding hardcoded
`Thread.Sleep()` calls and making tests more reliable and faster
across different environments.

### Centralised Base Test
`BaseTest.cs` in the Tests folder manages shared setup (browser launch)
and teardown (browser close) inherited by all test classes — keeping
individual test files focused on test logic only.

## Design Decisions
- **Operation-specific models** — separate Model classes for Add,
  Edit, and Delete operations rather than one generic model, ensuring
  each operation only carries the data it needs
- **JSON over hardcoded data** — externalising test data into JSON
  files means new test scenarios can be added by updating data files
  without modifying code
- **ExtentReports** — chosen over basic NUnit reporting for richer,
  more readable HTML output that can be shared with stakeholders
- **Custom Wait utility** — centralised wait logic avoids duplication
  and makes synchronisation strategies easy to update across the
  whole framework
- **Screenshot on failure** — reduces investigation time by providing
  immediate visual evidence of what failed and why

## How to Run

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework / .NET Core installed
- Chrome browser installed
- ChromeDriver (managed via NuGet)

### Steps
1. Clone the repository
git clone https://github.com/S-Jeremiah/MarsOnboardReqnRoll.git
2. Open `MARSCOMPETITION.sln` in Visual Studio
3. Restore NuGet packages
Tools → NuGet Package Manager → Restore Packages
4. Build the solution
Build → Build Solution (Ctrl + Shift + B)
5. Run all tests
Test → Run All Tests (Ctrl + R, A)
6. View the HTML report
Open Report/report.html in any browser


## Skills Demonstrated
- Selenium WebDriver automation in C#
- NUnit test framework and assertions
- Page Object Model design pattern
- Operation-specific Model layer for typed data handling
- Data-driven testing with JSON and JsonReader utility
- ExtentReports HTML test reporting
- Screenshot capture on test failure
- Custom explicit wait implementation
- Scalable and maintainable enterprise-level framework architecture
- NuGet package management
