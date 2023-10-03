using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HartCheck_Admin.Models
{
    public class Admin : IdentityUser
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        [ForeignKey("auditLog")]
        public int auditLogID { get; set; }
    }
}
