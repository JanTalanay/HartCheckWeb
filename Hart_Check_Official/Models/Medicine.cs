using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class Medicine
    {
        [Key]
        public int medicineID { get; set; }
        public int consultationID { get; set; }
        public string medicine {  get; set; }
        public DateTime dateTime { get; set; }
        public double dosage { get; set; }

        public Consultation consultation { get; set; }
    }
}
