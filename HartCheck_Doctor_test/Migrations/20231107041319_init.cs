using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HartCheck_Doctor_test.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    adminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.adminID);
                });

            migrationBuilder.CreateTable(
                name: "DoctorLicense",
                columns: table => new
                {
                    licenseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<int>(type: "int", nullable: false),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    externalPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorLicense", x => x.licenseID);
                });

            migrationBuilder.CreateTable(
                name: "EducationalResource",
                columns: table => new
                {
                    resourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalResource", x => x.resourceID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    usersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    phoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.usersID);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Groups",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    auditLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    referenceID = table.Column<int>(type: "int", nullable: false),
                    tableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eventType = table.Column<int>(type: "int", nullable: false),
                    eventTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.auditLogID);
                    table.ForeignKey(
                        name: "FK_AuditLog_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "usersID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BugReport",
                columns: table => new
                {
                    bugID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usersID = table.Column<int>(type: "int", nullable: false),
                    featureID = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugReport", x => x.bugID);
                    table.ForeignKey(
                        name: "FK_BugReport_Users_usersID",
                        column: x => x.usersID,
                        principalTable: "Users",
                        principalColumn: "usersID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthCareProfessional",
                columns: table => new
                {
                    doctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usersID = table.Column<int>(type: "int", nullable: false),
                    clinic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    licenseID = table.Column<int>(type: "int", nullable: true),
                    verification = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCareProfessional", x => x.doctorID);
                    table.ForeignKey(
                        name: "FK_HealthCareProfessional_DoctorLicense_licenseID",
                        column: x => x.licenseID,
                        principalTable: "DoctorLicense",
                        principalColumn: "licenseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthCareProfessional_Users_usersID",
                        column: x => x.usersID,
                        principalTable: "Users",
                        principalColumn: "usersID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    patientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.patientID);
                    table.ForeignKey(
                        name: "FK_Patients_Users_usersID",
                        column: x => x.usersID,
                        principalTable: "Users",
                        principalColumn: "usersID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: true),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    OrdinalPriority = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Resources",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditLogValue",
                columns: table => new
                {
                    auditLogID = table.Column<int>(type: "int", nullable: false),
                    columnName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    oldValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    newValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogValue", x => x.auditLogID);
                    table.ForeignKey(
                        name: "FK_AuditLogValue_AuditLog_auditLogID",
                        column: x => x.auditLogID,
                        principalTable: "AuditLog",
                        principalColumn: "auditLogID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthCareClinic",
                columns: table => new
                {
                    healthCareClinicID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorID = table.Column<int>(type: "int", nullable: false),
                    clinicID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCareClinic", x => x.healthCareClinicID);
                    table.ForeignKey(
                        name: "FK_HealthCareClinic_HealthCareProfessional_doctorID",
                        column: x => x.doctorID,
                        principalTable: "HealthCareProfessional",
                        principalColumn: "doctorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArchievedRecord",
                columns: table => new
                {
                    archivedRecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    systolic = table.Column<int>(type: "int", nullable: false),
                    diastolic = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchievedRecord", x => x.archivedRecordID);
                    table.ForeignKey(
                        name: "FK_ArchievedRecord_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodPressure",
                columns: table => new
                {
                    bloodPressureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    systolic = table.Column<double>(type: "float", nullable: false),
                    diastolic = table.Column<double>(type: "float", nullable: false),
                    dateTaken = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressure", x => x.bloodPressureID);
                    table.ForeignKey(
                        name: "FK_BloodPressure_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodPressureThreshold",
                columns: table => new
                {
                    thresholdID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    doctorID = table.Column<int>(type: "int", nullable: false),
                    systolicLevel = table.Column<double>(type: "float", nullable: false),
                    diastolicLevel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressureThreshold", x => x.thresholdID);
                    table.ForeignKey(
                        name: "FK_BloodPressureThreshold_HealthCareProfessional_doctorID",
                        column: x => x.doctorID,
                        principalTable: "HealthCareProfessional",
                        principalColumn: "doctorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodPressureThreshold_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BodyMass",
                columns: table => new
                {
                    bodyMassID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    BMITypeID = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<int>(type: "int", nullable: false),
                    height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMass", x => x.bodyMassID);
                    table.ForeignKey(
                        name: "FK_BodyMass_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultation",
                columns: table => new
                {
                    consultationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorSchedID = table.Column<int>(type: "int", nullable: false),
                    patientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultation", x => x.consultationID);
                    table.ForeignKey(
                        name: "FK_Consultation_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCondition",
                columns: table => new
                {
                    medCondID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    medicalCondition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conditionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCondition", x => x.medCondID);
                    table.ForeignKey(
                        name: "FK_MedicalCondition_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistory",
                columns: table => new
                {
                    medicalHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    medicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pastSurgicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistory", x => x.medicalHistoryID);
                    table.ForeignKey(
                        name: "FK_MedicalHistory_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientsDoctor",
                columns: table => new
                {
                    patientDoctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    doctorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsDoctor", x => x.patientDoctorID);
                    table.ForeignKey(
                        name: "FK_PatientsDoctor_HealthCareProfessional_doctorID",
                        column: x => x.doctorID,
                        principalTable: "HealthCareProfessional",
                        principalColumn: "doctorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientsDoctor_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreviousMedication",
                columns: table => new
                {
                    prevMedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    previousMed = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreviousMedication", x => x.prevMedID);
                    table.ForeignKey(
                        name: "FK_PreviousMedication_Patients_patientID",
                        column: x => x.patientID,
                        principalTable: "Patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    clinicID = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.clinicID);
                    table.ForeignKey(
                        name: "FK_Clinic_HealthCareClinic_clinicID",
                        column: x => x.clinicID,
                        principalTable: "HealthCareClinic",
                        principalColumn: "healthCareClinicID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BMIType",
                columns: table => new
                {
                    BMITypeID = table.Column<int>(type: "int", nullable: false),
                    BMI = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BMIType", x => x.BMITypeID);
                    table.ForeignKey(
                        name: "FK_BMIType_BodyMass_BMITypeID",
                        column: x => x.BMITypeID,
                        principalTable: "BodyMass",
                        principalColumn: "bodyMassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    chatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consultationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.chatID);
                    table.ForeignKey(
                        name: "FK_Chat_Consultation_consultationID",
                        column: x => x.consultationID,
                        principalTable: "Consultation",
                        principalColumn: "consultationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    conditionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consultationID = table.Column<int>(type: "int", nullable: false),
                    condition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.conditionID);
                    table.ForeignKey(
                        name: "FK_Condition_Consultation_consultationID",
                        column: x => x.consultationID,
                        principalTable: "Consultation",
                        principalColumn: "consultationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    diagnosisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consultationID = table.Column<int>(type: "int", nullable: false),
                    diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => x.diagnosisID);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Consultation_consultationID",
                        column: x => x.consultationID,
                        principalTable: "Consultation",
                        principalColumn: "consultationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedule",
                columns: table => new
                {
                    doctorSchedID = table.Column<int>(type: "int", nullable: false),
                    doctorID = table.Column<int>(type: "int", nullable: false),
                    schedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedule", x => x.doctorSchedID);
                    table.ForeignKey(
                        name: "FK_DoctorSchedule_Consultation_doctorSchedID",
                        column: x => x.doctorSchedID,
                        principalTable: "Consultation",
                        principalColumn: "consultationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSchedule_HealthCareProfessional_doctorID",
                        column: x => x.doctorID,
                        principalTable: "HealthCareProfessional",
                        principalColumn: "doctorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    medicineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    consultationID = table.Column<int>(type: "int", nullable: false),
                    medicine = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.medicineID);
                    table.ForeignKey(
                        name: "FK_Medicine_Consultation_consultationID",
                        column: x => x.consultationID,
                        principalTable: "Consultation",
                        principalColumn: "consultationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    messagesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chatID = table.Column<int>(type: "int", nullable: false),
                    recieverID = table.Column<int>(type: "int", nullable: false),
                    senderID = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateSent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.messagesID);
                    table.ForeignKey(
                        name: "FK_Messages_Chat_chatID",
                        column: x => x.chatID,
                        principalTable: "Chat",
                        principalColumn: "chatID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchievedRecord_patientID",
                table: "ArchievedRecord",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_userID",
                table: "AuditLog",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressure_patientID",
                table: "BloodPressure",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressureThreshold_doctorID",
                table: "BloodPressureThreshold",
                column: "doctorID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressureThreshold_patientID",
                table: "BloodPressureThreshold",
                column: "patientID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BodyMass_patientID",
                table: "BodyMass",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_BugReport_usersID",
                table: "BugReport",
                column: "usersID");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_consultationID",
                table: "Chat",
                column: "consultationID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Condition_consultationID",
                table: "Condition",
                column: "consultationID");

            migrationBuilder.CreateIndex(
                name: "IX_Consultation_patientID",
                table: "Consultation",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_consultationID",
                table: "Diagnosis",
                column: "consultationID");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedule_doctorID",
                table: "DoctorSchedule",
                column: "doctorID");

            migrationBuilder.CreateIndex(
                name: "IX_HealthCareClinic_doctorID",
                table: "HealthCareClinic",
                column: "doctorID");

            migrationBuilder.CreateIndex(
                name: "IX_HealthCareProfessional_licenseID",
                table: "HealthCareProfessional",
                column: "licenseID",
                unique: true,
                filter: "[licenseID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HealthCareProfessional_usersID",
                table: "HealthCareProfessional",
                column: "usersID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCondition_patientID",
                table: "MedicalCondition",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_patientID",
                table: "MedicalHistory",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_consultationID",
                table: "Medicine",
                column: "consultationID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_chatID",
                table: "Messages",
                column: "chatID");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_usersID",
                table: "Patients",
                column: "usersID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsDoctor_doctorID",
                table: "PatientsDoctor",
                column: "doctorID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsDoctor_patientID",
                table: "PatientsDoctor",
                column: "patientID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreviousMedication_patientID",
                table: "PreviousMedication",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_GroupId",
                table: "Resources",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ResourceId",
                table: "WorkOrders",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ArchievedRecord");

            migrationBuilder.DropTable(
                name: "AuditLogValue");

            migrationBuilder.DropTable(
                name: "BloodPressure");

            migrationBuilder.DropTable(
                name: "BloodPressureThreshold");

            migrationBuilder.DropTable(
                name: "BMIType");

            migrationBuilder.DropTable(
                name: "BugReport");

            migrationBuilder.DropTable(
                name: "Clinic");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.DropTable(
                name: "DoctorSchedule");

            migrationBuilder.DropTable(
                name: "EducationalResource");

            migrationBuilder.DropTable(
                name: "MedicalCondition");

            migrationBuilder.DropTable(
                name: "MedicalHistory");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PatientsDoctor");

            migrationBuilder.DropTable(
                name: "PreviousMedication");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "BodyMass");

            migrationBuilder.DropTable(
                name: "HealthCareClinic");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "HealthCareProfessional");

            migrationBuilder.DropTable(
                name: "Consultation");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "DoctorLicense");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
