using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class ArchievedRecord
    {
        [Key]
        public int archievedRecordID { get; set; }
        public int patientID { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime birthdate { get; set; }
        public int sytolic { get; set; }
        public int diastolic { get; set; }

        public Patients patients { get; set; }
    }
}
