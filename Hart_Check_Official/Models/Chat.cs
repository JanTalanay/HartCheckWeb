using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
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
