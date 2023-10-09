using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IUserRepository
    {
        ICollection<Users>GetUser();

        Users GetUsers(int userID);

        bool UserExists(int userID); 

        Task<bool> CreateUsersAsync(Users users);
        bool CreateUsers(Users users);
        bool LoginUsers(Users users);
        bool UpdateUsers(Users users);

        bool DeleteUser(Users users);

        bool Save();
    }
}
 