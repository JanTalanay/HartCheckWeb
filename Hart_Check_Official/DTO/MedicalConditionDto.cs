using Hart_Check_Official.Models;

namespace Hart_Check_Official.DTO
{
    public class MedicalConditionDto
    {
        public int medCondID { get; set; }
        public int patientID { get; set; }
        public string medicalCondition { get; set; }
        public string conditionName { get; set; }
    }
}
