using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class BMIType
    {
        [Key]
        public int BMITypeID { get; set; }
        public string BMI { get; set; }

        public BodyMass BodyMass { get; set; }
    }
}
