using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IMedicalHistoryRepository
    {
        ICollection<MedicalHistory> GetMedicalHistories();

        MedicalHistory GetMedicalHistory(int medicalHistoryID);

        MedicalHistory GetMedicalHistoryPatientID(int patientID);
        bool medHistoryExsist(int medicalHistoryID);
        bool medHistoryExsistPatientID(int patientID);
        MedicalHistory CreatemedHistory(MedicalHistory medicalHistory);
        bool DeletemedHistory(MedicalHistory medicalHistory);
        bool UpdatemedHistory(MedicalHistory medicalHistory);

        bool Save();
    }
}
