using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class DoctorLicense
    {
        [Key]
        public int licenseID { get; set; }
        public int status { get; set; }
        public string fileName { get; set; }
        public string externalPath { get; set; }


        public HealthCareProfessional doctor { get; set; }
    }
}
