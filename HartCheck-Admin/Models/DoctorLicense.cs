using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HartCheck_Admin.Models
{
    [Table("DoctorLicense")]
    public class DoctorLicense
    {
        [Key]
        public int licenseID { get; set; }
        public int status { get; set; }
        public string fileName { get; set; }
        public string externalPath { get; set; }
    }
}
