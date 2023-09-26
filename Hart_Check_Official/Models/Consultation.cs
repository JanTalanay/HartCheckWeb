using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class Consultation
    {
        [Key]
        public int consultationID { get; set; }
        public int doctorSchedID { get; set; }
        public int patientID { get; set; }


        public ICollection<Patients> patients { get; set; }
        public ICollection<DoctorSchedule> doctorsched { get; set; }
        public Condition condition { get; set; }
        public Diagnosis diagnosis { get; set; }
        public Medicine medicine { get; set; }
        public Chat chat { get; set; }

    }
}
