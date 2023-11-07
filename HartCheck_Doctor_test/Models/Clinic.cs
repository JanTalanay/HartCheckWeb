using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class Clinic
    {
        [Key]
        public int clinicID { get; set; }
        public string name { get; set; }
        public string location { get; set; }

        public HealthCareClinic HealthcareClinic { get; set; }

    }
}
