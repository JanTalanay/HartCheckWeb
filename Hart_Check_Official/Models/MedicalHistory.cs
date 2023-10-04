using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class MedicalHistory
    {
        [Key]
        public int medicalHistoryID { get; set; }
        public int patientID { get; set; }
        public string medicalHistory {  get; set; }
        public string pastSurgicalHistory { get; set; }

        public Patients patients { get; set; }

    }
}
