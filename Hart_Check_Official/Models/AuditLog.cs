using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class AuditLog
    {
        [Key]
        public int auditLogID { get; set; }
        public int patientID { get; set; }
        public int doctorID { get; set; }
        public int adminID { get; set; }
        public int referenceID { get; set; }
        public string tableName { get; set; }
        public int eventType { get; set; }
        public DateTime? eventTimeStamp { get; set; }


        public ICollection<Patients> patients { get; set; }
        public ICollection<HealthCareProfessional> doctor { get; set; }
        public ICollection<Admin> admin { get; set; }
        public ICollection<AuditLogValue> auditlogsvalue { get; set; }


    }
}
