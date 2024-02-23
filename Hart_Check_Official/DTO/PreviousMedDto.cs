namespace Hart_Check_Official.DTO
{
    public class PreviousMedDto
    {
        public int prevMedID { get; set; }
        public int patientID { get; set; }
        public string previousMed { get; set; }
        public double dosage { get; set; }
        public DateTime date { get; set; }
    }
}
