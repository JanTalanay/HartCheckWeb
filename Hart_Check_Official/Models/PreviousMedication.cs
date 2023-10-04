using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
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
