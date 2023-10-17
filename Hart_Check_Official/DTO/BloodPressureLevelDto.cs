using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.DTO
{
    public class BloodPressureLevelDto
    {
        public int usersID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Status { get; set; }

        public long Stages { get; set; }

        public long SystolicMin { get; set; }

        public long SystolicMax { get; set; }

        public long DiastolicMin { get; set; }

        public long DiastolicMax { get; set; }

        public static implicit operator BloodPressureLevelDto(DoctorEditProfileDto v)
        {
            throw new NotImplementedException();
        }
    }
}
