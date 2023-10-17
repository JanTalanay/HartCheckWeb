using Hart_Check_Official.DTO;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IDoctorEditProfileRepository
    {

        ICollection<DoctorEditProfileDto> GetUser();

        Users GetUsers(int userID);

        bool UpdateDoctorProfile(Users Users);
        bool UserExists(int userID);

        bool Save();
        bool UpdateDoctorProfile(DoctorEditProfileDto doctorEditProfileMap);
    }
}
