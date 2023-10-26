using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IUserRepository
    {
        ICollection<Users>GetUser();

        Users GetUsers(int userID);
        Users GetUsersEmail(String email);

        bool UserExists(int userID); 
        bool UserExistsEmail(String email);

        Task<Users> CreateUsersAsync(Users users);
        bool CreateUsers(Users users);
        Users LoginUsers(Login login);
        bool UpdateUsers(Users users);

        bool DeleteUser(Users users);

        bool Save();
    }
}
 