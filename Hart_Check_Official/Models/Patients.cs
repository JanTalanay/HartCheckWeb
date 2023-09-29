using System.ComponentModel.DataAnnotations;

namespace Hart_Check_Official.Models
{
    public class Patients
    {
        [Key]
        public int patientID { get; set; }
        public int usersID { get; set; }


        public Users User { get; set; }
        public PatientsDoctor patientDoctor { get; set; }
        public BodyMass BodyMass { get; set; }
        public BloodPressureThreshold BloodPressureThreshold { get; set; }
        public MedicalCondition MedicalConditions { get; set; }
        public PreviousMedication PreviousMedication { get; set; }
        public MedicalHistory MedicalHistory { get; set; }
        public BloodPressure BloodPressure { get; set; }
        public Consultation Consultation { get; set; }
        public ArchievedRecord archievedrecord { get; set; }
        public AuditLog auditlog { get; set; }
    }
}
