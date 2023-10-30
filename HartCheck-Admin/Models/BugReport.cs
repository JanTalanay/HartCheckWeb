using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HartCheck_Admin.Models
{
    [Table("BugReport")]
    public class BugReport
    {
        [Key]
        public int bugID { get; set; }
        [ForeignKey("User")]
        public int userID { get; set; }
        public int featureID { get; set; }
        public string description { get; set; }

    }
}
