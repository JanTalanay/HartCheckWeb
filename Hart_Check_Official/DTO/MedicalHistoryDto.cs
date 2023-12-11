namespace Hart_Check_Official.DTO
{
    public class MedicalHistoryDto
    {
        public int medicalHistoryID { get; set; }
        public int patientID { get; set; }
        public string medicalHistory { get; set; }
        public string pastSurgicalHistory { get; set; }
        public DateTime date { get; set; }
    }
}
