using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Model
{
    public class DeleteEducation
    {

        public string Scenario { get; set; } = string.Empty;
        public string College { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;


        public class DeleteEducationDataSet
        {
            public List<DeleteEducation> Educations { get; set; } = new List<DeleteEducation>();
        }
    }
}
