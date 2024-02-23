using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class ArchievedRecord
    {
        [Key]
        public int archivedRecordID { get; set; }
        public int patientID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime birthdate { get; set; }
        public int systolic { get; set; }
        public int diastolic { get; set; }

        public Patients patients { get; set; }
    }
}
