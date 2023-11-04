using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IBloodPressureThresholdRepository
    {
        BloodPressureThreshold GetBloodPressureThreshold(int patientID);
        bool PatientExists(int patientID);
    }
}
