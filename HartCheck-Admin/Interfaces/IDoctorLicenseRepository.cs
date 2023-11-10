using HartCheck_Admin.Models;

namespace HartCheck_Admin.Interfaces
{
    public interface IDoctorLicenseRepository
    {
        Task<IEnumerable<DoctorLicense>> GetAll();
        Task<DoctorLicense> GetByIdAsync(int id);
        Task<DoctorLicense> GetByIdAsyncNoTracking(int id);
        Task<DoctorLicense> GetLicenseByUserIdAsync(int userId);
        bool Add(DoctorLicense license);
        bool Update(DoctorLicense license);
        bool Delete(DoctorLicense license);
        bool Save();

    }
}
