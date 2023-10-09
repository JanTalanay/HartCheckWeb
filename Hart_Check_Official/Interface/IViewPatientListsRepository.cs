using Hart_Check_Official.DTO;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IViewPatientListsRepository
    {
        ICollection<ViewPatientDto> GetUser();

        Users GetUsers(int userID);

        bool UserExists(int userID);

        bool Save();
    }
}
