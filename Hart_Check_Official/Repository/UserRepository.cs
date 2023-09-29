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

        public bool CreateUsers(Users users)
        {

            _context.Add(users);
            return Save();
        }

        public bool DeleteUser(Users users)
        {
            _context.Remove(users);
            return Save();
        }

        public ICollection<Users> GetUser()
        {
            return _context.Users.OrderBy(p => p.usersID).ToList();
        }

        public Users GetUsers(int userID)
        {
            return _context.Users.Where(e => e.usersID == userID).FirstOrDefault();
        }

        public int GetUsersRole(int patientID)
        {
            var patient = _context.Patients.Where(e => e.User.usersID == patientID);

            if (patient.Count() <= 0)
            {
                return 0;
            }
            return patient.Count();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUsers(Users users)
        {
            _context.Update(users);
            return Save();
        }

        public bool UserExists(int userID)
        {
            return _context.Users.Any(e => e.usersID == userID);
        }
    }
}
