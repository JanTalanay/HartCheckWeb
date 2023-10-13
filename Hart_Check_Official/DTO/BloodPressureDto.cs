namespace Hart_Check_Official.DTO
{
    public class BloodPressureDto
    {
        public int bloodPressureID { get; set; }
        public int patientID { get; set; }
        public int systolic { get; set; }
        public int diastolic { get; set; }
        public DateTime? dateTaken { get; set; }
    }
}
