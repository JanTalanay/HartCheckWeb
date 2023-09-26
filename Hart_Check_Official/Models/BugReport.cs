using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class BugReport
    {
        [Key]
        public int bugID { get; set; }
        public int usersID { get; set; }
        public ICollection<Users> User { get; set; }
        public int featureID { get; set; }
        public string description { get; set; }
    }
}
