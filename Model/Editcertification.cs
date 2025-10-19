using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Model
{
    public class Editcertification
    {
        public string Scenario { get; set; } = string.Empty;
        public string ExistingCertificate { get; set; } = string.Empty;

        public string NewCertificate { get; set; } = string.Empty;

        public string ExistingCertifiedFrom { get; set; } = string.Empty;
        public string NewCertifiedFrom { get; set; } = string.Empty;

        public string ExistingYear { get; set; } = string.Empty;
        public string NewYear { get; set; } = string.Empty; 
        
       
        public string ExpectedResult { get; set; } = string.Empty;
    }

    public class EditCertificationDataSet
    {
        public List<Editcertification> Certifications { get; set; } = new List<Editcertification>();
    }








}





