using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class BloodPressure
    {
        [Key]
        public int bloodPressureID { get; set; }
        public int patientID { get; set; }
        public double systolic { get; set; }
        public double diastolic { get; set; }
        public DateTime? dateTaken { get; set; }

        public Patients patients { get; set; }
    }
}
