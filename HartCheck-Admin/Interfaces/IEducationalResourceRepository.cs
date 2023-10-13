using HartCheck_Admin.Models;

namespace HartCheck_Admin.Interfaces
{
    public interface IEducationalResourceRepository
    {
        Task<IEnumerable<EducationalResource>> GetAll();
        Task<EducationalResource> GetByIdAsync(int id);
        Task<EducationalResource> GetByIdAsyncNoTracking(int id);

        bool Add(EducationalResource educationalResource);
        bool Update(EducationalResource educationalResource);  
        bool Delete(EducationalResource educationalResource);
        bool Save();
    }
}
