using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class BodyMass
    {
        [Key]
        public int bodyMassID { get; set; }
        public int patientID { get; set; }
        public int BMITypeID { get; set; }
        public float weight { get; set; }
        public float height { get; set; }

        public BMIType BMIType { get; set; }
        public ICollection<Patients> patient { get; set; }
    }
}
