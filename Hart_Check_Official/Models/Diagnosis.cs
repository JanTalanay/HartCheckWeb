using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class Diagnosis
    {
        [Key]
        public int diagnosisID { get; set; }
        public int consultationID { get; set; }
        public string diagnosis { get; set; }
        public DateTime dateTime { get; set; }

        public Consultation consultation { get; set; }
    }
}
