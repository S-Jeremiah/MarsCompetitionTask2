using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Model
{
    public class EditEducationData
    {
        public string Scenario { get; set; } = string.Empty;
        public string ExistingCollege { get; set; } = string.Empty;

        public string ExistingCountry { get; set; } = string.Empty;
        public string ExistingTitle { get; set; } = string.Empty;
        public string NewCollege { get; set; } = string.Empty;
       

        public string NewCountry { get; set; } = string.Empty;

        public string NewTitle { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;  
        public string Year { get; set; } = string.Empty;    


        public string ExpectedResult { get; set; } = string.Empty;

    }

    public class EditEducationDataSet
    {
        public List<EditEducationData> Educations { get; set; } = new List<EditEducationData>();
    }

}



