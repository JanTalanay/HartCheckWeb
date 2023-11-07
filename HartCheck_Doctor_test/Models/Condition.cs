using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class Condition
    {
        [Key]
        public int conditionID { get; set; }
        public int consultationID { get; set; }
        public string condition {  get; set; }

        public Consultation consultation { get; set; }
    }
}
