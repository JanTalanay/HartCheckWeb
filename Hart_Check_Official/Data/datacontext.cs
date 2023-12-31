﻿using Hart_Check_Official.DTO;
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
        public DbSet<DoctorSchedule> DoctorSchedule { get; set; }
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
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Resource> Resources { get; set; }

        /*public DbSet<ViewPatientDto> ViewPatientDto { get; set; }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BMIType>()
                .HasOne(e => e.BodyMass)
                .WithOne(e => e.BMIType)
                .HasForeignKey<BMIType>(e => e.BMITypeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BugReport>()
                .HasOne(e => e.User)
                .WithMany(e => e.bugreport)
                .HasForeignKey(e => e.usersID)
                .OnDelete(DeleteBehavior.Cascade);


            //Doctor
            modelBuilder.Entity<HealthCareProfessional>()
                .HasOne(e => e.User)
                .WithMany(e => e.doctor)
                .HasForeignKey(e => e.usersID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HealthCareClinic>()
                .HasOne(e => e.doctor)
                .WithMany(e => e.HealthcareClinic)
                .HasForeignKey(e => e.doctorID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HealthCareProfessional>()
                .HasOne(e => e.BloodPressureThreshold)
                .WithOne(e => e.doctor)
                .HasForeignKey<BloodPressureThreshold>(e => e.doctorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientsDoctor>()
                .HasOne(e => e.doctor)
                .WithMany(e => e.patientDoctor)
                .HasForeignKey(e => e.doctorID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HealthCareProfessional>()//goods
                .HasOne(e => e.Doctorlicense)
                .WithOne(e => e.doctor)
                .HasForeignKey<HealthCareProfessional>(e => e.licenseID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DoctorSchedule>()//goods
                .HasOne(e => e.doctor)
                .WithMany(e => e.DoctorSchedule)
                .HasForeignKey(e => e.doctorID)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<AuditLog>()//goods
            //    .HasOne(e => e.doctor)
            //    .WithMany(e => e.auditlog)
            //    .HasForeignKey(e => e.doctorID);

            /////
            modelBuilder.Entity<Clinic>()
                .HasOne(e => e.HealthcareClinic)
                .WithMany(e => e.clinic)
                .HasForeignKey(e => e.clinicID)
                .OnDelete(DeleteBehavior.Cascade);

            //Patient
            modelBuilder.Entity<Patients>()
                .HasOne(e => e.User)
                .WithMany(e => e.patients)
                .HasForeignKey(e => e.usersID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientsDoctor>()
                .HasOne(e => e.patient)
                .WithOne(e => e.patientDoctor)
                .HasForeignKey<PatientsDoctor>(e => e.patientID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BodyMass>()
                .HasOne(e => e.patient)
                .WithMany(e => e.BodyMass)
                .HasForeignKey(e => e.patientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BloodPressureThreshold>()
                .HasOne(e => e.patients)
                .WithOne(e => e.BloodPressureThreshold)
                .HasForeignKey<BloodPressureThreshold>(e => e.patientID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalCondition>()
                .HasOne(e => e.patients)
                .WithMany(e => e.MedicalConditions)
                .HasForeignKey(e => e.patientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PreviousMedication>()
                .HasOne(e => e.patients)
                .WithMany(e => e.PreviousMedication)
                .HasForeignKey(e => e.patientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicalHistory>()
                .HasOne(e => e.patients)
                .WithMany(e => e.MedicalHistory)
                .HasForeignKey(e => e.patientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BloodPressure>()
                .HasOne(e => e.patients)
                .WithMany(e => e.BloodPressure)
                .HasForeignKey(e => e.patientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArchievedRecord>()
                .HasOne(e => e.patients)
                .WithMany(e => e.archievedrecord)
                .HasForeignKey(e => e.patientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consultation>()
                .HasOne(e => e.patients)
                .WithMany(e => e.Consultation)
                .HasForeignKey(e => e.patientID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consultation>()
                .HasMany(e => e.doctorsched)
                .WithOne(e => e.consultation)
                .HasForeignKey(e => e.doctorSchedID)
                .OnDelete(DeleteBehavior.Cascade);

            ////consultation child
            modelBuilder.Entity<Diagnosis>()
                .HasOne(e => e.consultation)
                .WithMany(e => e.diagnoses)
                .HasForeignKey(e => e.consultationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Condition>()
                .HasOne(e => e.consultation)
                .WithMany(e => e.conditions)
                .HasForeignKey(e => e.consultationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medicine>()
                .HasOne(e => e.consultation)
                .WithMany(e => e.medicines)
                .HasForeignKey(e => e.consultationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>()
                .HasOne(e => e.consultations)
                .WithOne(e => e.chat)
                .HasForeignKey<Chat>(e => e.consultationID)
                .OnDelete(DeleteBehavior.Cascade);

            //chat and message
            modelBuilder.Entity<Chat>()
                .HasMany(e => e.messages)
                .WithOne(e => e.chat)
                .HasForeignKey(e => e.chatID)
                .OnDelete(DeleteBehavior.Cascade);

            //audits
            modelBuilder.Entity<AuditLog>()
                .HasOne(e => e.users)
                .WithMany(e => e.auditlog)
                .HasForeignKey(e => e.userID)
                .OnDelete(DeleteBehavior.Cascade);



            //modelBuilder.Entity<AuditLog>()
            //    .HasOne(e => e.admin)
            //    .WithMany(e => e.auditlog)
            //    .HasForeignKey(e => e.adminID);

            modelBuilder.Entity<AuditLogValue>()
                .HasOne(e => e.auditlog)
                .WithMany(e => e.auditlogsvalue)
                .HasForeignKey(e => e.auditLogID)
                .OnDelete(DeleteBehavior.Cascade);

            //calendar 
            // Configure Group-Resource relationship
            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Group)
                .WithMany(g => g.Resources)
                .HasForeignKey(r => r.GroupId)
                .HasConstraintName("FK_Resources_Groups");

            // Configure Resource-WorkOrder relationship
            modelBuilder.Entity<WorkOrder>()
                .HasOne(w => w.Resource)
                .WithMany()
                .HasForeignKey(w => w.ResourceId)
                .HasConstraintName("FK_WorkOrders_Resources");
        }
    }
}
