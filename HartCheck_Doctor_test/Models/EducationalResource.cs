using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class EducationalResource
    {
        [Key]
        public int resourceID {  get; set; }
        public string text { get; set; }
        public string link { get; set; }

    }
}
