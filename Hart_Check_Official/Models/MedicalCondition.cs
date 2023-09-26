using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class MedicalCondition
    {
        [Key]
        public int medicalConditionID { get; set; }
        public int patientID { get; set; }
        public string medicalCondition { get; set; }
        public string conditionName { get; set; }


        public ICollection<Patients> patients { get; set; }

    }
}
