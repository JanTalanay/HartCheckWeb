using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HartCheck_Admin.Data
{
    public class ApplicationDbContext : IdentityDbContext<Admin>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<EducationalResource> EducationalResources { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}
