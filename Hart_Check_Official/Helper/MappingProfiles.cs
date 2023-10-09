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

            CreateMap<BodyMass, BodyMassDto>();
            CreateMap<BodyMassDto, BodyMass>();

            CreateMap<MedicalCondition, MedicalConditionDto>();
            CreateMap<MedicalConditionDto, MedicalCondition>();

            CreateMap<PreviousMedication, PreviousMedDto>();
            CreateMap<PreviousMedDto, PreviousMedication>();

            CreateMap<MedicalHistory, MedicalConditionDto>();
            CreateMap<MedicalConditionDto, MedicalHistory>();
        }
    }
   
}
