using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IConsultationRepository
    {
        ICollection<Consultation> GetConsultations();

        Consultation GetConsultation(int consultationID);
        Consultation GetConsultationPatientsID(int patientID);

        //Consultation GetBugReport(string description);
        bool consultationExists(int consultationID);
        bool consultationExistsPatientsID(int patientID);

        bool CreateConsultation(Consultation consultation);
        bool DeleteConsultation(Consultation consultation);
        bool UpdateConsultation(Consultation consultation);

        bool Save();
    }
}
