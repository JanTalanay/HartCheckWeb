using HartCheck_Admin.Data;
using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace HartCheck_Admin.Repository
{
    public class HCProfessionalRepository : IHCProfessionalRepository
    {
        private readonly ApplicationDbContext _context;
        public HCProfessionalRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(HCProfessional hcprofessional)
        {
            _context.Add(hcprofessional);
            return Save();
        }

        public bool Delete(HCProfessional hcprofessional)
        {
            _context.Remove(hcprofessional);
            return Save();
        }

        public async Task<IEnumerable<HCProfessional>> GetAll()
        {
            return await _context.HCProfessionals.ToListAsync();
        }

        public async Task<HCProfessional> GetByIdAsync(int id)
        {
            return await _context.HCProfessionals.FirstOrDefaultAsync(i => i.doctorID == id);
        }
        public async Task<HCProfessional> GetByIdAsyncNoTracking(int id)
        {
            return await _context.HCProfessionals.AsNoTracking().FirstOrDefaultAsync(i => i.doctorID == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(HCProfessional hcprofessional)
        {
            _context.Update(hcprofessional);
            return Save();
        }

        public async Task<IEnumerable<HCProfessional>> GetHealthcareProfessionalsWithVerification()
        {
            var healthcareProfessionals = await _context.HCProfessionals
                .Where(h => h.verification == 1)
                .ToListAsync();

            return healthcareProfessionals;
        }
    }
}
