using HartCheck_Admin.Models;

namespace HartCheck_Admin.Interfaces
{
    public interface IHCProfessionalRepository
    {
        Task<IEnumerable<HCProfessional>> GetAll();
        Task<HCProfessional> GetByIdAsync(int id);
        Task<HCProfessional> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<HCProfessional>> GetHealthcareProfessionalsWithVerification();
        Task<IEnumerable<HCProfessional>> GetHealthcareProfessionalsWithNoVerification();
        Task<HCProfessional> GetProfessionalByUserIdAsync(int userId);
        bool Add(HCProfessional hcprofessional);
        bool Update(HCProfessional hcprofessional);
        bool Delete(HCProfessional hcprofessional);
        bool Save();
       
    }
}
