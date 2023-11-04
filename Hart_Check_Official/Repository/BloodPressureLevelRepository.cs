using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;

namespace Hart_Check_Official.Repository
{
    public class BloodPressureLevelRepository
    {
        private readonly datacontext _context;
        public BloodPressureLevelRepository(datacontext context)
        {
            _context = context;
        }

        public async Task UpdateBloodPressureLevelAsync(BloodPressureLevelDto updatedbloodpressurelevel)
        {
            // Retrieve the existing doctor profile from the database
            BloodPressureLevelDto bloodpressurelevel = await GetBloodPressureLevelAsync();

            // Update the properties of the existing profile with the values from the updated profile
            bloodpressurelevel.FirstName = updatedbloodpressurelevel.FirstName + " " + updatedbloodpressurelevel.LastName;
            bloodpressurelevel.Status = updatedbloodpressurelevel.Status;
            bloodpressurelevel.Stages = updatedbloodpressurelevel.Stages;
            bloodpressurelevel.SystolicMin = updatedbloodpressurelevel.SystolicMin;
            bloodpressurelevel.SystolicMax = updatedbloodpressurelevel.SystolicMax;
            bloodpressurelevel.DiastolicMin = updatedbloodpressurelevel.DiastolicMin;
            bloodpressurelevel.DiastolicMax = updatedbloodpressurelevel.DiastolicMax;


            // Save the updated profile back to the database
            await SaveBloodPressureLevelAsync(bloodpressurelevel);
        }

        private Task SaveBloodPressureLevelAsync(BloodPressureLevelDto existingProfile)
        {
            throw new NotImplementedException();
        }

        private Task<BloodPressureLevelDto> GetBloodPressureLevelAsync()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
