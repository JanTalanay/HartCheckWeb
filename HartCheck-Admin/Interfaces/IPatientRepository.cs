using HartCheck_Admin.Models;

namespace HartCheck_Admin.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAll();
        Task<Patient> GetByIdAsync(int id);
        Task<Patient> GetByIdAsyncNoTracking(int id);

        bool Add(Patient patient);
        bool Update(Patient patient);  
        bool Delete(Patient patient);
        bool Save();
    }
}
