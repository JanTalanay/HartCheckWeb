using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class BMIType
    {
        [Key]
        public int BMITypeID { get; set; }
        public string BMITypeName { get;}

        public BodyMass BodyMass { get; set; }
    }
}
