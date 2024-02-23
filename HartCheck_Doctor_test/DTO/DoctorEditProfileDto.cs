using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.DTO
{
    public class DoctorEditProfileDto
    {
        public int usersID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Gender { get; set; }

        [Display(Name = "Birth Date")]
        public string BirthDate { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        //public string Hospital { get; set; }

        [Display(Name = "Phone Number")]
        public long PhoneNumber { get; set; }

        //[Display(Name = "License Number")]
        //public string LicenseNo { get; set; }    
    }
}
