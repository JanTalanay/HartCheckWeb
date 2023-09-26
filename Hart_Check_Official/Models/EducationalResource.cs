using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class EducationalResource
    {
        [Key]
        public int resourceID {  get; set; }
        public string text { get; set; }
        public string link { get; set; }

    }
}
