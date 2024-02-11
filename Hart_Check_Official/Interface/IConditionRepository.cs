using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IConditionRepository
    {
        ICollection<Condition> GetConditions();
        Condition GetCondition(int conditionID);
        bool ConditionExist(int conditionID);
        ICollection<Condition> GetConditionsByPatientId(int patientId);

    }
}
