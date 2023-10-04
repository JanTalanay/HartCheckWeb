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
        public ICollection<BodyMass> BodyMass { get; set; }
        public BloodPressureThreshold BloodPressureThreshold { get; set; }
        public ICollection<MedicalCondition> MedicalConditions { get; set; }
        public ICollection<PreviousMedication> PreviousMedication { get; set; }
        public ICollection<MedicalHistory> MedicalHistory { get; set; }
        public ICollection<BloodPressure> BloodPressure { get; set; }
        public ICollection<Consultation> Consultation { get; set; }
        public ICollection<ArchievedRecord> archievedrecord { get; set; }
        public ICollection<AuditLog> auditlog { get; set; }
    }
}
