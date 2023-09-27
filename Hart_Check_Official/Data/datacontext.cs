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
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BMIType>()
                .HasOne(e => e.BodyMass)
                .WithOne(e => e.BMIType)
                .HasForeignKey<BMIType>(e => e.BMITypeID)
                .IsRequired();

            modelBuilder.Entity<BugReport>()//many to one
                .HasOne(e => e.User)
                .WithMany(e => e.bugreport)
                .HasForeignKey(e => e.usersID)
                .IsRequired();

            modelBuilder.Entity<Patients>()//one to  one
                .HasOne(e => e.User)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.usersID)
                .IsRequired();

            modelBuilder.Entity<HealthCareProfessional>()
                .HasOne(e => e.User)
                .WithMany(e => e.doctor)
                .HasForeignKey(e => e.usersID)
                .IsRequired();

            //Doctor
            modelBuilder.Entity<HealthCareClinic>()
                .HasMany(e => e.doctor)
                .WithOne(e => e.HealthcareClinic)
                .HasForeignKey(e => e.doctorID);

            modelBuilder.Entity<HealthCareProfessional>()
                .HasOne(e => e.BloodPressureThreshold)
                .WithOne(e => e.doctor)
                .HasForeignKey<HealthCareProfessional>(e => e.doctorID);

            modelBuilder.Entity<HealthCareClinic>()
                .HasMany(e => e.clinic)
                .WithOne(e => e.HealthcareClinic)
                .HasForeignKey(e => e.clinicID);

            modelBuilder.Entity<DoctorLicense>()
                .HasOne(e => e.doctor)
                .WithOne(e => e.Doctorlicense)
                .HasForeignKey<DoctorLicense>(e => e.licenseID);

            modelBuilder.Entity<DoctorSchedule>()
                .HasMany(e => e.doctor)
                .WithOne(e => e.DoctorSchedule)
                .HasForeignKey(e => e.doctorID);

            //Patient
            modelBuilder.Entity<PatientsDoctor>()
                .HasOne(e  => e.patient)
                .WithOne(e => e.patientDoctor)
                .HasForeignKey<PatientsDoctor>(e => e.patientID);

            modelBuilder.Entity<BodyMass>()
                .HasMany(e => e.patient)
                .WithOne(e => e.BodyMass)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Patients>()
                .HasOne(e => e.BloodPressureThreshold)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Patients>()
                .HasOne(e => e.MedicalConditions)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Patients>()
                .HasOne(e => e.PreviousMedication)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Patients>()
                .HasOne(e => e.MedicalHistory)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Patients>()
                .HasOne(e => e.BloodPressure)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Patients>()
                .HasOne(e => e.Consultation)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<Patients>()
                .HasOne(e => e.archievedrecord)
                .WithOne(e => e.patients)
                .HasForeignKey<Patients>(e => e.patientID);

            modelBuilder.Entity<Consultation>()
                .HasMany(e => e.doctorsched)
                .WithOne(e => e.consultation)
                .HasForeignKey(e => e.doctorScheduleID);

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
                .HasMany(e => e.patients)
                .WithOne(e => e.auditlog)
                .HasForeignKey(e => e.patientID);

            modelBuilder.Entity<AuditLog>()
                .HasMany(e => e.doctor)
                .WithOne(e => e.auditlog)
                .HasForeignKey(e => e.doctorID);

            modelBuilder.Entity<AuditLog>()
                .HasMany(e => e.admin)
                .WithOne(e => e.auditlog)
                .HasForeignKey(e => e.adminID);

            modelBuilder.Entity<AuditLogValue>()
                .HasOne(e => e.auditlog)
                .WithMany(e => e.auditlogsvalue)
                .HasForeignKey(e => e.auditLogID);
        }
    }
}
