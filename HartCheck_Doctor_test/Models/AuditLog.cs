using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class AuditLog
    {
        [Key]
        public int auditLogID { get; set; }
        public int userID { get; set; }
        public int referenceID { get; set; }
        public string tableName { get; set; }
        public int eventType { get; set; }
        public DateTime? eventTimeStamp { get; set; }


        //public Patients patients { get; set; }
        //public HealthCareProfessional doctor { get; set; }
        //public Admin admin { get; set; }
        public Users users { get; set; }
        public ICollection<AuditLogValue> auditlogsvalue { get; set; }


    }
}
