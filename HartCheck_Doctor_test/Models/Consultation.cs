using System.ComponentModel.DataAnnotations;

namespace HartCheck_Doctor_test.Models
{
    public class Consultation
    {
        [Key]
        public int consultationID { get; set; }
        public int doctorSchedID { get; set; }
        public int patientID { get; set; }


        public Patients patients { get; set; }
        public ICollection<DoctorSchedule> doctorsched { get; set; }
        public ICollection<Condition> conditions { get; set; }
        public ICollection<Diagnosis> diagnoses { get; set; }
        public ICollection<Medicine> medicines { get; set; }
        public Chat chat { get; set; }

    }
}
