using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARSCOMPETITION.Model
{
    public class DeleteCertification
    {
        public string Scenario { get; set; } = string.Empty;
        public string Certificate { get; set; } = string.Empty;
        public string CertifiedFrom { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;
    }

    public class DeleteCertificationDataSet
    {
        public List<DeleteCertification> Certifications { get; set; } = new List<DeleteCertification>();

    }
}
