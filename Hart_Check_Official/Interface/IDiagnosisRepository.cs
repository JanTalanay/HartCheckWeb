using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IDiagnosisRepository
    {
        ICollection<Diagnosis> GetDiagnoses();
        ICollection<Diagnosis> GetDiagnosisByPatientId(int patientID);
        Diagnosis GetDiagnose(int diagnosisID);
        bool DiagnoseExist(int diagnosisID);
    }
}
