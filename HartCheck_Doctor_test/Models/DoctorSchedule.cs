using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class DoctorSchedule
    {
        [Key]
        public int doctorSchedID { get; set; }
        public int doctorID { get; set; }
        public DateTime schedDateTime { get; set; }


        public HealthCareProfessional doctor { get; set; }
        public Consultation consultation { get; set; }
    }
}
