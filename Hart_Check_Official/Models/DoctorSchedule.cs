using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
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
