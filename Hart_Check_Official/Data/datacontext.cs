using Hart_Check_Official.Models;
using Microsoft.EntityFrameworkCore; 

namespace Hart_Check_Official.Data
{
    public class datacontext : DbContext
    {
        public datacontext(DbContextOptions<datacontext> options) : base(options)
        {

        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<ArchievedRecord> ArchievedRecord { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
        public DbSet<AuditLogValue> AuditLogValue { get; set; }
        public DbSet<BloodPressure> BloodPressure { get; set; }
        public DbSet<BloodPressureThreshold> BloodPressureThreshold { get; set; }
        public DbSet<BMIType> BMIType { get; set; }
        public DbSet<BodyMass> BodyMass { get; set; }
        public DbSet<BugReport> BugReport { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<Condition> Condition { get; set; }
        public DbSet<Consultation> Consultation { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<DoctorLicense> DoctorLicense { get; set; }
        public DbSet<EducationalResource> EducationalResource { get; set; }
        public DbSet<HealthCareClinic> HealthCareClinic { get; set; }
        public DbSet<HealthCareProfessional> HealthCareProfessional { get; set; }
        public DbSet<MedicalCondition> MedicalCondition { get; set; }
        public DbSet<MedicalHistory> MedicalHistory { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<PatientsDoctor> PatientsDoctor { get; set; }
        public DbSet<PreviousMedication> PreviousMedication { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BMIType>()
                .HasOne(e => e.BodyMass)
                .WithOne(e => e.BMIType)
                .HasForeignKey<BMIType>(e => e.BMITypeID);

            modelBuilder.Entity<BugReport>()
                .HasOne(e => e.User)
                .WithMany(e => e.bugreport)
                .HasForeignKey(e => e.usersID);


            //Doctor
            modelBuilder.Entity<HealthCareProfessional>()
                .HasOne(e => e.User)
                .WithMany(e => e.doctor)
                .HasForeignKey(e => e.usersID);

            modelBuilder.Entity<HealthCareClinic>()
                .HasOne(e => e.doctor)
                .WithMany(e => e.HealthcareClinic)
                .HasForeignKey(e => e.doctorID);

            modelBuilder.Entity<HealthCareProfessional>()
                .HasOne(e => e.BloodPressureThreshold)
                .WithOne(e => e.doctor)
                .HasForeignKey<BloodPressureThreshold>(e => e.doctorID);

            modelBuilder.Entity<PatientsDoctor>()
                .HasOne(e => e.doctor)
                .WithMany(e => e.patientDoctor)
                .HasForeignKey(e => e.doctorID);

            modelBuilder.Entity<HealthCareProfessional>()//goods
                .HasOne(e => e.Doctorlicense)
                .WithOne(e => e.doctor)
                .HasForeignKey<HealthCareProfessional>(e => e.licenseID);

            modelBuilder.Entity<DoctorSchedule>()//goods
                .HasOne(e => e.doctor)
                .WithMany(e => e.DoctorSchedule)
                .HasForeignKey(e => e.doctorID);

            modelBuilder.Entity<AuditLog>()//goods
                .HasOne(e => e.doctor)
                .WithMany(e => e.auditlog)
                .HasForeignKey(e => e.doctorID);

            /////
            modelBuilder.Entity<Clinic>()
                .HasOne(e => e.HealthcareClinic)
                .WithMany(e => e.clinic)
                .HasForeignKey(e => e.clinicID);

            //Patient
            modelBuilder.Entity<Patients>()
                .HasOne(e => e.User)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.usersID);

            modelBuilder.Entity<PatientsDoctor>()
                .HasOne(e => e.patient)
                .WithOne(e => e.patientDoctor)
                .HasForeignKey<PatientsDoctor>(e => e.patientID);

            modelBuilder.Entity<BodyMass>()
                .HasOne(e => e.patient)
                .WithMany(e => e.BodyMass)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<BloodPressureThreshold>()
                .HasOne(e => e.patients)
                .WithOne(e => e.BloodPressureThreshold)
                .HasForeignKey<BloodPressureThreshold>(e => e.patientID);

            modelBuilder.Entity<MedicalCondition>()
                .HasOne(e => e.patients)
                .WithMany(e => e.MedicalConditions)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<PreviousMedication>()
                .HasOne(e => e.patients)
                .WithMany(e => e.PreviousMedication)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<MedicalHistory>()
                .HasOne(e => e.patients)
                .WithMany(e => e.MedicalHistory)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<BloodPressure>()
                .HasOne(e => e.patients)
                .WithMany(e => e.BloodPressure)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Consultation>()
                .HasOne(e => e.patients)
                .WithMany(e => e.Consultation)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<ArchievedRecord>()
                .HasOne(e => e.patients)
                .WithMany(e => e.archievedrecord)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Consultation>()
                .HasMany(e => e.doctorsched)
                .WithOne(e => e.consultation)
                .HasForeignKey(e => e.doctorSchedID);

            //consultation child
            modelBuilder.Entity<Consultation>()
                .HasOne(e => e.diagnosis)
                .WithMany(e => e.consultations)
                .HasForeignKey(e => e.consultationID);

            modelBuilder.Entity<Consultation>()
                .HasOne(e => e.condition)
                .WithMany(e => e.consultations)
                .HasForeignKey(e => e.consultationID);

            modelBuilder.Entity<Consultation>()
                .HasOne(e => e.medicine)
                .WithMany(e => e.consultations)
                .HasForeignKey(e => e.consultationID);

            modelBuilder.Entity<Consultation>()
                .HasOne(e => e.chat)
                .WithOne(e => e.consultations)
                .HasForeignKey<Consultation>(e => e.consultationID);

            //chat and message
            modelBuilder.Entity<Chat>()
                .HasMany(e => e.messages)
                .WithOne(e => e.chat)
                .HasForeignKey(e => e.chatID);

            //audits
            modelBuilder.Entity<AuditLog>()
                .HasOne(e => e.patients)
                .WithMany(e => e.auditlog)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<AuditLog>()
                .HasOne(e => e.admin)
                .WithMany(e => e.auditlog)
                .HasForeignKey(e => e.adminID);

            modelBuilder.Entity<AuditLogValue>()
                .HasOne(e => e.auditlog)
                .WithMany(e => e.auditlogsvalue)
                .HasForeignKey(e => e.auditLogID);
        }
    }
}
