using Hart_Check_Official.DTO;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IBloodPressureLevelRepository
    {
        ICollection<DoctorEditProfileDto> GetUser();

        Users GetUsers(int userID);

        bool UpdateBloodPressureLevel(Users Users);
        bool UserExists(int userID);

        bool Save();
        bool UpdateBloodPressureLevel(DoctorEditProfileDto doctorEditProfileMap);
        bool updateBloodPressureLevel(Users bPlevelMap);
    }
}
