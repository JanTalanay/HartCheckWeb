using System.ComponentModel.DataAnnotations;

namespace HartCheck_Admin.Models
{
    public class EducationalResource
    {
        [Key]
        public int resourceID { get; set; }
        public string text { get; set; }
        public string link { get; set; }
    }
}
