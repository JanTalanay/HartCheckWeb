using HartCheck_Admin.Models;

namespace HartCheck_Admin.Interfaces
{
    public interface IBugReportRepository
    {
        Task<IEnumerable<BugReport>> GetAll();
        Task<BugReport> GetByIdAsync(int id);
        Task<BugReport> GetByIdAsyncNoTracking(int id);

        bool Add(BugReport bugReport);
        bool Update(BugReport bugReport);
        bool Delete(BugReport bugReport);
        bool Save();
    }
}