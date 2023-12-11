using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class Condition
    {
        [Key]
        public int conditionID { get; set; }
        public int consultationID { get; set; }
        public string condition {  get; set; }
        public DateTime date { get; set; }
        public Consultation consultation { get; set; }
    }
}
