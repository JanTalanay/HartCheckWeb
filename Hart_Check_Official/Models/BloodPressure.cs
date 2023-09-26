using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class BloodPressure
    {
        [Key]
        public int bloodPressureID { get; set; }
        public int patientID { get; set; }
        public int sytolic { get; set; }
        public int diastolic { get; set; }
        public DateTime? dateTaken { get; set; }

        public ICollection<Patients> patients { get; set; }
    }
}
