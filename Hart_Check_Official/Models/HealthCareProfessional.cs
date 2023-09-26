using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class HealthCareProfessional
    {
        [Key]
        public int doctorID { get; set; }
        public int usersID { get; set; }
        public int clinicID { get; set; }
        public int licenseID { get; set; }
        public int verification {  get; set; }


        public Users User { get; set; }
        public Patients patients { get; set; }
        public HealthCareClinic HealthcareClinic { get; set; }
        public BloodPressureThreshold BloodPressureThreshold { get; set; }
        public DoctorLicense Doctorlicense { get; set; }
        public DoctorSchedule DoctorSchedule { get; set; }
        public AuditLog auditlog { get; set; }
    }
}
