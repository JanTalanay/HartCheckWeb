using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IMedicalConditionRepository
    {
        //get the data based on the patientID
        //delete or update based on their original ID
        ICollection<MedicalCondition> GetMedicalConditions();

        MedicalCondition GetMedicalCondition(int medCondID);
        MedicalCondition GetMedicalConditionPatientID(int patientID);

        bool MedicalConditionExists(int medCondID);
        bool MedicalConditionExistspatientID(int patientID);

        bool CreateMedicalCondition(MedicalCondition medicalCondition);
        bool UpdateMedicalCondition(MedicalCondition medicalCondition);
        bool DeleteMedicalCondition(MedicalCondition medicalCondition);
        bool Save();
    }
}
