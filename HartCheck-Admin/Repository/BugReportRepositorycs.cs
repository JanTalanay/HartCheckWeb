using HartCheck_Admin.Data;
using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace HartCheck_Admin.Repository
{
    public class BugReportRepository : IBugReportRepository
    {
        private readonly ApplicationDbContext _context;
        public BugReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(BugReport bugReport)
        {
            _context.Add(bugReport);
            return Save();
        }

        public bool Delete(BugReport bugReport)
        {
            _context.Remove(bugReport);
            return Save();
        }

        public async Task<IEnumerable<BugReport>> GetAll()
        {
            return await _context.BugReports.ToListAsync();
        }

        public async Task<BugReport> GetByIdAsync(int id)
        {
            return await _context.BugReports.FirstOrDefaultAsync(i => i.bugID == id);
        }
        public async Task<BugReport> GetByIdAsyncNoTracking(int id)
        {
            return await _context.BugReports.AsNoTracking().FirstOrDefaultAsync(i => i.bugID == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(BugReport bugReport)
        {
            _context.Update(bugReport);
            return Save();
        }
    }
}
