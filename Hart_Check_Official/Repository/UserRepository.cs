using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.EntityFrameworkCore;

namespace Hart_Check_Official.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly datacontext _context;

        public UserRepository(datacontext context)
        {
            _context = context;
        }

        public ICollection<Users> GetUser()
        {
            return _context.Users.OrderBy(p => p.usersID).ToList();
        }

        public Users GetUsers(int userID)
        {
            return _context.Users.Where(e => e.usersID == userID).FirstOrDefault();
        }

        public Users GetUsersEmail(string email)
        {
            return _context.Users.Where(e => e.email == email).FirstOrDefault();
        }

        public Users LoginUsers(Login login)
        {
            var user = _context.Users.SingleOrDefault(x => x.email == login.email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.password, user.password))
            {
                throw new Exception("Invalid email or password.");
            }

            return user;
        }

        public bool CreateUsers(Users users)
        {
            users.password = BCrypt.Net.BCrypt.HashPassword(users.password);
            _context.Add(users);
            return Save();
        }

        public async Task<Users> CreateUsersAsync(Users users)
        {
            try
            {
                users.password = BCrypt.Net.BCrypt.HashPassword(users.password);
                _context.Add(users);
                await _context.SaveChangesAsync();

                int userID = users.usersID;

                if (users.role == 1)
                {
                    var patient = new Patients
                    {
                        usersID = userID,
                    };
                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                }
                else if (users.role == 2)
                {
                    var doctor = new HealthCareProfessional
                    {
                        usersID = userID,
                        licenseID = null,
                        clinic = null,
                        verification = null,
                    };
                    _context.Add(doctor);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();

                return (users);
            }
            catch (Exception ex)
            {
                //Log the exception details
                return null;
            }
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

        public bool UserExistsEmail(string email)
        {
            return _context.Users.Any(e => e.email == email);
        }
        public bool DeleteUser(Users users)
        {
            _context.Users.Remove(users);
            // Save changes
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
