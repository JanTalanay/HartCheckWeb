using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HartCheck_Doctor_test.Models
{
    public class BugReport
    {
        [Key]
        public int bugID { get; set; }
        public int usersID { get; set; }
        public int featureID { get; set; }
        public string description { get; set; }

        public Users User { get; set; }
    }
}
