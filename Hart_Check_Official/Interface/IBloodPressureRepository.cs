using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IBloodPressureRepository
    {
        //get the data based on the patientID
        //delete or update based on their original ID
        ICollection<BloodPressure> GetBloodPressures();

        ICollection<BloodPressure> GetBloodPressPatientID(int patientID);
        //BloodPressure GetBloodPressPatientID(int patientID);
        BloodPressure GetBloodPress(int bloodPressureID);

        bool BloodPressureExistPatientID(int patientID);
        bool BloodPressureExist(int bloodPressureID);
        BloodPressure CreateBloodPressure(BloodPressure bloodpressure);

        bool UpdateBloodPressure(BloodPressure bloodpressure);

        bool DeleteBloodPressure(BloodPressure bloodpressure);

        bool Save();
    }
}
