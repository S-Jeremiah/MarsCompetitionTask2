using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Model
{
    public class LoginTestData
    {
        public string Scenario { get; set; } = string.Empty;
        public string EmailID { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;

    }
    public class LoginDataSet
    {
        public List<LoginTestData> Logins { get; set; } = new List<LoginTestData>();
        
    }
}
