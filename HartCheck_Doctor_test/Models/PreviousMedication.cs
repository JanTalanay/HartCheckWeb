using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class PreviousMedication
    {
        [Key]
        public int prevMedID { get; set; }
        public int patientID { get; set; }
        public string previousMed {  get; set; }

        public Patients patients { get; set; }
    }
}
