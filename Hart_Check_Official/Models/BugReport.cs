using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hart_Check_Official.Models
{
    public class BugReport
    {
        [Key]
        public int bugID { get; set; }
        public int usersID { get; set; }
        public int featureID { get; set; }
        public string description { get; set; }

        public ICollection<Users> User { get; set; }
    }
}
