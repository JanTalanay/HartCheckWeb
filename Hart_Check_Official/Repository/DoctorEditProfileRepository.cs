using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class DoctorEditProfileRepository : IDoctorEditProfileRepository
    {
        private readonly datacontext _context;
        public DoctorEditProfileRepository(datacontext context)
        {
            _context = context;
        }

        public async Task UpdateDoctorProfileAsync(DoctorEditProfileDto updatedProfile)
        {
            // Retrieve the existing doctor profile from the database
            DoctorEditProfileDto existingProfile = await GetDoctorProfileAsync();

            // Update the properties of the existing profile with the values from the updated profile
            existingProfile.FirstName = updatedProfile.FirstName;
            existingProfile.LastName = updatedProfile.LastName;
            existingProfile.Gender = updatedProfile.Gender;
            existingProfile.BirthDate = updatedProfile.BirthDate;
            existingProfile.Email = updatedProfile.Email;
            //existingProfile.Hospital = updatedProfile.Hospital;
            existingProfile.PhoneNumber = updatedProfile.PhoneNumber;
            //existingProfile.LicenseNo = updatedProfile.LicenseNo;

            // Save the updated profile back to the database
            await SaveDoctorProfileAsync(existingProfile);
        }

        private Task SaveDoctorProfileAsync(DoctorEditProfileDto existingProfile)
        {
            throw new NotImplementedException();
        }

        private Task<DoctorEditProfileDto> GetDoctorProfileAsync()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<DoctorEditProfileDto> GetUser()
        {
            throw new NotImplementedException();
        }

        public Users GetUsers(int userID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDoctorProfile(Users user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int userID)
        {
            return _context.Users.Any(u => u.usersID == userID);
            throw new NotImplementedException();
        }

        public bool UpdateDoctorProfile(DoctorEditProfileDto doctorEditProfileMap)
        {
            throw new NotImplementedException();
        }
    }
}
