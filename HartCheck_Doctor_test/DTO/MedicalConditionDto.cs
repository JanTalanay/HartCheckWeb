using HartCheck_Doctor_test.Models;

namespace HartCheck_Doctor_test.DTO
{
    public class MedicalConditionDto
    {
        public int medCondID { get; set; }
        public int patientID { get; set; }
        public string medicalCondition { get; set; }
        public string conditionName { get; set; }
    }
}
