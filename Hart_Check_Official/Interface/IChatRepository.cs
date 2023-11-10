using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IChatRepository
    {
        ICollection<Chat> GetChats();
        bool DeleteChat(Chat chat);
        bool UpdateChat(Chat chat);

        bool Save();
    }
}
