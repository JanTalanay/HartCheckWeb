using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class AuditLogValue
    {
        [Key]
        public int auditLogID {  get; set; }
        public string columnName { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }

        public AuditLog auditlog { get; set; }
    }
}
