using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Hart_Check_Official.Models;
using Hart_Check_Official.DTO;

namespace WebApp_Doctor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }

        public DbSet<Users> ViewPatients { get; set; }

    }   
}
