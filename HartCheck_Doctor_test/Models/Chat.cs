using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class Chat
    {
        [Key]
        public int chatID { get; set; }
        public int consultationID { get; set; }

        public Consultation consultations { get; set; }
        public ICollection<Messages> messages { get; set; }

    }
}
