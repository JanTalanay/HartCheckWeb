using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class BodyMass
    {
        [Key]
        public int bodyMassID { get; set; }
        public int patientID { get; set; }
        public int BMITypeID { get; set; }
        public int weight { get; set; }
        public int height { get; set; }

        public BMIType BMIType { get; set; }
        public Patients patient { get; set; }
    }
}
