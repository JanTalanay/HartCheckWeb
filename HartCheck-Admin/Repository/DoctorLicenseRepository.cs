using HartCheck_Admin.Data;
using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using HartCheck_Admin.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HartCheck_Admin.Repository
{
    public class DoctorLicenseRepository : IDoctorLicenseRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorLicenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(DoctorLicense license)
        {
            _context.Add(license);
            return Save();
        }

        public bool Delete(DoctorLicense license)
        {
            _context.Remove(license);
            return Save();
        }

        public async Task<IEnumerable<DoctorLicense>> GetAll()
        {
            return await _context.DoctorLicenses.ToListAsync();
        }

        public async Task<DoctorLicense> GetByIdAsync(int id)
        {
            return await _context.DoctorLicenses.FirstOrDefaultAsync(i => i.licenseID == id);
        }
        public async Task<DoctorLicense> GetByIdAsyncNoTracking(int id)
        {
            return await _context.DoctorLicenses.AsNoTracking().FirstOrDefaultAsync(i => i.licenseID == id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(DoctorLicense license)
        {
            _context.Update(license);
            return Save();
        }

        public async Task<DoctorLicense> GetLicenseByUserIdAsync(int userId)
        {
            return await _context.HCProfessionals
                .Where(hcp => hcp.userID == userId)
                .Join(
                    _context.DoctorLicenses,
                    hcp => hcp.licenseID,
                    dl => dl.licenseID,
                    (hcp, dl) => dl
                 )
                .FirstOrDefaultAsync();
        }

    }
}
