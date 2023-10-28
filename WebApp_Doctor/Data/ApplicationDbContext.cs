using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Hart_Check_Official.Models;
using Hart_Check_Official.DTO;

namespace WebApp_Doctor.Data
{
    public class ApplicationDbContext : DbContext //this is the right one, I just remove for the sake of scafold
    /*public class ApplicationDbContext : IdentityDbContext<LoginDto>*/
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }

        public DbSet<Users> ViewPatients { get; set; }
        /*public DbSet<LoginDto> Login { get; set; }*/
        public DbSet<WorkOrder> WorkOrders { get; set; }

    }
}
