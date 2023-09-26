using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IUserRepository
    {
        ICollection<Users>GetUsers();
    }
}
