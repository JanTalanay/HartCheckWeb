using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IUserRepository
    {
        ICollection<Users>GetUser();

        Users GetUsers(int userID);

        int GetUsersRole (int patientID);

        bool UserExists(int userID); 

        bool CreateUsers(Users users);
        bool UpdateUsers(Users users);

        bool DeleteUser(Users users);

        bool Save();
    }
}
 