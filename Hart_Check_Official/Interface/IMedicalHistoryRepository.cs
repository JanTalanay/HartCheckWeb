using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IMedicalHistoryRepository
    {
        ICollection<MedicalHistory> GetMedicalHistories();

        MedicalHistory GetMedicalHistory(int medicalHistoryID);

        MedicalHistory GetMedHistory(string pastSurgicalHistory);
        bool medHistoryExsist(int medicalHistoryID);
        MedicalHistory CreatemedHistory(MedicalHistory medicalHistory);
        bool DeletemedHistory(MedicalHistory medicalHistory);
        bool UpdatemedHistory(MedicalHistory medicalHistory);

        bool Save();
    }
}
