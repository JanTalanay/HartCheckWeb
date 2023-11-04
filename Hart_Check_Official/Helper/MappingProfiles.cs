using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Users, UserDto>();
            CreateMap<UserDto, Users>();

            CreateMap<BugReport, BugReportDto>();
            CreateMap<BugReportDto, BugReport>();

            CreateMap<LoginDto, Users>();
            CreateMap<Users, LoginDto>();

            CreateMap<Patients, PatientDto>();
            CreateMap<PatientDto, Patients>();

            CreateMap<BloodPressure, BloodPressureDto>();
            CreateMap<BloodPressureDto, BloodPressure>();

            CreateMap<BodyMass, BodyMassDto>();
            CreateMap<BodyMassDto, BodyMass>();

            CreateMap<MedicalCondition, MedicalConditionDto>();
            CreateMap<MedicalConditionDto, MedicalCondition>();

            CreateMap<PreviousMedication, PreviousMedDto>();
            CreateMap<PreviousMedDto, PreviousMedication>();

            CreateMap<MedicalHistory, MedicalHistoryDto>();
            CreateMap<MedicalHistoryDto, MedicalHistory>();

            CreateMap<BMIType, BMITypeDto>();
            CreateMap<BMITypeDto, BMIType>();

            CreateMap<HealthCareProfessional, HealthCareProfessionalDto>();
            CreateMap<HealthCareProfessionalDto, HealthCareProfessional>();

            CreateMap<DoctorSchedule, DoctorScheduleDto>();
            CreateMap<DoctorScheduleDto, DoctorSchedule>();

            CreateMap<PatientsDoctor, PatientsDoctorDto>();
            CreateMap<PatientsDoctorDto, PatientsDoctor>();


            CreateMap<Consultation, ConsultationDto>();
            CreateMap<ConsultationDto, Consultation>();

            /*CreateMap<Users, ViewPatientDto>();
            CreateMap<ViewPatientDto, Users>();*/

            CreateMap<Users, DoctorEditProfileDto>();
            CreateMap<DoctorEditProfileDto, Users>();

            CreateMap<EducationalResource, EducationalResourceDto>();
            CreateMap<EducationalResourceDto, EducationalResource>();

            CreateMap<BloodPressureThreshold, BloodPressureThresholdDto>();
            CreateMap<BloodPressureThresholdDto, BloodPressureThreshold>();
        }
    }
   
}
