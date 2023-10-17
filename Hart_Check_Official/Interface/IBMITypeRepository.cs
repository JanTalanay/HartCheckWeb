using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IBMITypeRepository
    {
        ICollection<BMIType> GetBMITypes();

        BMIType GetBMI(int BMITypeID);

        bool BMITypeExists(int BMITypeID);

        bool Save();

    }
}
