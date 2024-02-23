using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class Diagnosis
    {
        [Key]
        public int diagnosisID { get; set; }
        public int consultationID { get; set; }
        public string diagnosis { get; set; }

        public Consultation consultation { get; set; }
    }
}
