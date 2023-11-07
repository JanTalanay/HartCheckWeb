using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class Medicine
    {
        [Key]
        public int medicineID { get; set; }
        public int consultationID { get; set; }
        public string medicine {  get; set; }

        public Consultation consultation { get; set; }
    }
}
