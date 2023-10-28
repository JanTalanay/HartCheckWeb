using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hart_Check_Official.Models
{
    public class HealthCareProfessional
    {
        [Key]
        public int doctorID { get; set; }
        public int usersID { get; set; }
        public string? clinic { get; set; }
        public int? licenseID { get; set; }
        public int? verification {  get; set; }

        public Users User { get; set; }
        public ICollection<PatientsDoctor> patientDoctor { get; set; }
        public ICollection<HealthCareClinic> HealthcareClinic { get; set; }
        public BloodPressureThreshold BloodPressureThreshold { get; set; }
        public DoctorLicense Doctorlicense { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedule { get; set; }
        //public ICollection<AuditLog> auditlog { get; set; }
    }
}
