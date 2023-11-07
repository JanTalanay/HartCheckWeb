using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class MedicalCondition
    {
        [Key]
        public int medCondID { get; set; }
        public int patientID { get; set; }
        public string medicalCondition { get; set; }
        public string conditionName { get; set; }


        public Patients patients { get; set; }

    }
}
