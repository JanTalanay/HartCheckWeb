using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IUserRepository
    {
        ICollection<Users>GetUser();

        Users GetUsers(int userID);

        bool UserExists(int userID); 

        Task<Users> CreateUsersAsync(Users users);
        Users CreateUsers(Users users);
        Users LoginUsers(Login login);
        bool UpdateUsers(Users users);

        bool DeleteUser(Users users);

        bool Save();
    }
}
 