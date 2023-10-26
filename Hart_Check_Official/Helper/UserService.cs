using Hart_Check_Official.Models;
using Microsoft.AspNetCore.Identity;

namespace Hart_Check_Official.Helper
{
    public class UserService
    {
        private readonly PasswordHasher<Users> _passwordHasher;

        public UserService(PasswordHasher<Users> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public Users CreateUser(string email, string firstName, string lastName, string password, DateTime birthdate, int gender, long phoneNumber, int role)
        {
            var user = new Users
            {
                email = email,
                firstName = firstName,
                lastName = lastName,
                birthdate = birthdate,
                gender = gender,
                phoneNumber = phoneNumber,
                role = role
            };

            user.password = _passwordHasher.HashPassword(user, password);

            return user;
        }

        public void UpdateUser(Users user, string password)
        {
            user.password = _passwordHasher.HashPassword(user, password);
        }
    }
}
