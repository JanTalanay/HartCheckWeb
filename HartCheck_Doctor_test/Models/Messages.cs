using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class Messages
    {
        [Key]
        public int messagesID { get; set; }
        public int chatID { get; set; }
        public int recieverID { get; set; }
        public int senderID { get; set; }
        public string content { get; set; }
        public DateTime dateSent { get; set; }

        public Chat chat { get; set; }
    }
}
