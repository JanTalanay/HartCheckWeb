using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class DoctorSchedule
    {
        [Key]
        public int doctorScheduleID { get; set; }
        public int doctorID { get; set; }
        public DateTime schedDateTime { get; set; }


        public ICollection<HealthCareProfessional> doctor { get; set; }
        public Consultation consultation { get; set; }
    }
}
