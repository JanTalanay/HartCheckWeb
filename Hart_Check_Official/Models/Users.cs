using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Hart_Check_Official.Models
{
    public class Users
    {
        //many is always ICOLLECTION
        //one is calling to the Model
        [Key]
        public int usersID { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public DateTime birthdate { get; set; }
        public int gender { get; set; }
        public long phoneNumber { get; set; }
        public int role { get; set; }


        public BugReport BugReports { get; set; }
        public Patients patients { get; set; }
        public HealthCareProfessional doctor { get; set; }

    }
}
