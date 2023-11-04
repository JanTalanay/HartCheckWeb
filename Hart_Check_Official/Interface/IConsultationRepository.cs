using Hart_Check_Official.DTO;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IConsultationRepository
    {
        ICollection<Consultation> GetConsultations();

        Consultation GetConsultation(int consultationID);
        Consultation GetConsultationdoctorSchedID(int doctorSchedID);
        Consultation GetConsultationPatientsID(int patientID);

        //Consultation GetBugReport(string description);
        bool consultationExists(int consultationID);
        bool consultationExistsdoctorSchedID(int doctorSchedID);
        bool consultationExistsPatientsID(int patientID);
        DoctorSchedule GetDoctorScheduleByID(int doctorSchedID);
        HealthCareProfessional GetHealthCareProfessionalByID(int doctorID);
        Consultation CreateConsultation(Consultation consultation);
        Users GetDoctorUserByDoctorId(int doctorID);

        bool DeleteConsultation(Consultation consultation);
        bool UpdateConsultation(Consultation consultation);

        bool Save();
        ICollection<DoctorScheduleDto> GetDoctorSchedulesForPatient(int patientID);

    }

}
