using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IConsultationRepository
    {
        ICollection<Consultation> GetConsultations();

        Consultation GetConsultation(int consultationID);

        //Consultation GetBugReport(string description);
        bool consultationExists(int consultationID);

        bool CreateConsultation(Consultation consultation);
        bool DeleteConsultation(Consultation consultation);
        bool UpdateConsultation(Consultation consultation);

        bool Save();
    }
}
