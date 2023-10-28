using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class Admin
    {
        [Key]
        public int adminID { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        //public ICollection<AuditLog> auditlog { get; set; }
    }
}
