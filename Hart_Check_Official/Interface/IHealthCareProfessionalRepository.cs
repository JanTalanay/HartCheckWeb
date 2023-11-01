using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IHealthCareProfessionalRepository
    {
        ICollection<HealthCareProfessional> GetHealthCareProfessionals();

        HealthCareProfessional GetHealthCareProfessional(int doctorID);

        bool HealthCareProfessionalExist(int doctorID);

        HealthCareProfessional CreateHealthCareProfessional(HealthCareProfessional doctorID);
        bool DeletepHealthCareProfessional(HealthCareProfessional doctorID);
        bool UpdateHealthCareProfessional(HealthCareProfessional doctorID);

        bool Save();
    }
}
