using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IBloodPressureRepository
    {
        ICollection<BloodPressure> GetBloodPressures();

        BloodPressure GetBloodPress(int bloodPressureID);

        bool BloodPressureExist(int bloodPressureID);
        bool CreateBloodPressure(BloodPressure bloodpressure);

        bool UpdateBloodPressure(BloodPressure bloodpressure);

        bool DeleteBloodPressure(BloodPressure bloodpressure);

        bool Save();
    }
}
