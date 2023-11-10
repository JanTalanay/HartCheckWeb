using AutoMapper;
using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly datacontext _context;

        private readonly IMapper _mapper;
        public ChatRepository(datacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool DeleteChat(Chat chat)
        {
            throw new NotImplementedException();
        }

        public ICollection<Chat> GetChats()
        {
            return _context.Chat.OrderBy(p => p.consultationID).ToList();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateChat(Chat chat)
        {
            throw new NotImplementedException();
        }
    }
}
