using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class MedicalCondition
    {
        [Key]
        public int medCondID { get; set; }
        public int patientID { get; set; }
        public string medicalCondition { get; set; }
        public DateTime date { get; set; }
        public Patients patients { get; set; }

    }
}
