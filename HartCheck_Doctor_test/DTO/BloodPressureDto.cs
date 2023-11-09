using System.Runtime.InteropServices;

namespace HartCheck_Doctor_test.DTO
{
    public class BloodPressureDto
    {
        public int bloodPressureID { get; set; }
        public int patientID { get; set; }
        public double systolic { get; set; }
        public double diastolic { get; set; }
        public DateTime? dateTaken { get; set; }
    }
}
