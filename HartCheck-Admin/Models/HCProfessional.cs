using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HartCheck_Admin.Models
{
    [Table("HealthcareProfessional")]
    public class HCProfessional
    {
        [Key]
        public int doctorID { get; set; }
        [ForeignKey("User")]
        public string userID { get; set; }
        public string clinic { get; set; }
        public string licenseID { get; set; }
        public int verification { get; set; }
    }
}
