using HartCheck_Admin.Data;
using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace HartCheck_Admin.Repository
{
    public class EducationalResourceRepository : IEducationalResourceRepository
    {
        private readonly ApplicationDbContext _context;
        public EducationalResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(EducationalResource educationalResource)
        {
            _context.Add(educationalResource);
            return Save();
        }

        public bool Delete(EducationalResource educationalResource)
        {
            _context.Remove(educationalResource);
            return Save();
        }

        public async Task<IEnumerable<EducationalResource>> GetAll()
        {
            return await _context.EducationalResources.ToListAsync();
        }

        public async Task<EducationalResource> GetByIdAsync(int id)
        {
            return await _context.EducationalResources.FirstOrDefaultAsync(i => i.resourceID == id);
        }
        public async Task<EducationalResource> GetByIdAsyncNoTracking(int id)
        {
            return await _context.EducationalResources.AsNoTracking().FirstOrDefaultAsync(i => i.resourceID == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(EducationalResource educationalResource)
        {
            _context.Update(educationalResource);
            return Save();
        }
    }
}
