using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly datacontext _context;

        public UserRepository(datacontext context)
        {
            _context = context;
        }

        public ICollection<Users> GetUsers()
        {
            return _context.Users.OrderBy(p => p.usersID).ToList();
        }
    }
}
