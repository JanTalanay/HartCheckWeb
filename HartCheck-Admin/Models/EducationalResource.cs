using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HartCheck_Admin.Models
{
    [Table("EducationalResource")]
    public class EducationalResource
    {
        [Key]
        public int resourceID { get; set; }
        public string text { get; set; }
        public string link { get; set; }
    }
}
