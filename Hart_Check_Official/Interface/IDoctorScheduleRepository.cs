using Hart_Check_Official.DTO;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IDoctorScheduleRepository
    {
        ICollection<DoctorSchedule> GetDoctorSchedules();

        DoctorSchedule GetDoctorSchedule(int doctorID);

        bool DoctorScheduleExist(int doctorID);
        bool PatientDoctorExist(int patientID);

        Task<(HealthCareProfessional, HealthCareProfessionalName)> GetDoctorDetails(int patientID);
        Task<List<DateTime>> GetDoctorSched(int doctorID);
        Task<List<(HealthCareProfessional, HealthCareProfessionalName, List<DateTime>)>> GetDoctorsDetailsAndSchedules(int patientID);
        Task<Dictionary<int, List<DateTime>>> GetDoctorSchedulesForPatient(int patientID);

        DoctorSchedule CreateDoctorSchedule(DoctorSchedule doctorSchedID);
        bool DeletepDoctorSchedule(DoctorSchedule doctorSchedID);
        bool UpdateDoctorSchedule(DoctorSchedule doctorSchedID);

        bool Save();
    }
}
