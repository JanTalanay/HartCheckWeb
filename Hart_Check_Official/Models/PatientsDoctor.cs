using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class PatientsDoctor
    {
        [Key]
        public int patientDoctorID { get; set; }
        public int patientID { get; set; }
        public int doctorID { get; set; }



        public Patients patient { get; set; }
        public HealthCareProfessional doctor { get; set; }

    }
}
