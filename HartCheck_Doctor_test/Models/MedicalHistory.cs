using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
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
