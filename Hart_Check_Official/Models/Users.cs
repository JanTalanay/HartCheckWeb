using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Hart_Check_Official.Models
{
    public class Users 
    {

        [Key]
        public int usersID { get; set; }

        //[Display(Name = "Email Name")]
        public string email { get; set; }

        //[Display(Name = "First Name")]
        public string firstName { get; set; }

        //[Display(Name = "Last Name")]
        public string lastName { get; set; }

        public string password { get; set; }
        public DateTime birthdate { get; set; }
        public int gender { get; set; }

        //[Display(Name = "Phone Number")]
        public long phoneNumber { get; set; }
        public int role { get; set; }


        public ICollection<BugReport> bugreport { get; set; }
        public ICollection<Patients> patients { get; set; }
        public ICollection<HealthCareProfessional> doctor { get; set; }

    }
}
