using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HartCheck_Admin.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<EducationalResource> EducationalResources { get; set; }
        public DbSet<User> Patients { get; set; }
        public DbSet<BugReport> BugReports { get; set; }
        public DbSet<HCProfessional> HCProfessionals { get; set; }
    }
}
