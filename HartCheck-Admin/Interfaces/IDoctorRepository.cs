using HartCheck_Admin.Models;

namespace HartCheck_Admin.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAll();
        Task<Doctor> GetByIdAsync(int id);
        Task<Doctor> GetByIdAsyncNoTracking(int id);

        bool Add(Doctor doctor);
        bool Update(Doctor doctor);
        bool Delete(Doctor doctor);
        bool Save();
    }
}
