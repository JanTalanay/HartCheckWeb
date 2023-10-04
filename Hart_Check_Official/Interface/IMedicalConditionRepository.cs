using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IMedicalConditionRepository
    {
        ICollection<MedicalCondition> GetMedicalConditions();

        MedicalCondition GetMedicalCondition(int medCondID);

        int GetMedicalConditionID(int medCondID);

        bool MedicalConditionExists(int medCondID);

        bool CreateMedicalCondition(MedicalCondition medicalCondition);
        bool UpdateMedicalCondition(MedicalCondition medicalCondition);
        bool DeleteMedicalCondition(MedicalCondition medicalCondition);
        Task<bool> CreateUsersAsync(MedicalCondition medicalCondition);
        bool Save();
    }
}
