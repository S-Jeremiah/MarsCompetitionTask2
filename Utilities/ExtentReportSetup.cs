using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Utilities
{
    [SetUpFixture]
    public class ExtentReportSetup
    {
        [OneTimeTearDown]
        public void TearDown()
        {
            ExtentManager.GetExtent().Flush();
        }
    }
}
