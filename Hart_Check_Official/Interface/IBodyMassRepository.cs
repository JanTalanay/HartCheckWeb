using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IBodyMassRepository
    {
        ICollection<BodyMass> GetBodies();

        BodyMass GetBodyMass(int bodyMassID);

        bool BodyMassExist(int bodyMassID);
        BodyMass CreateBodyMass(BodyMass bodyMass);

        Task<bool> CreateBodyMassAsync(BodyMass bodyMass);

        bool UpdateBodyMass(BodyMass bodyMass);

        bool DeleteBodyMass(BodyMass bodyMass);

        bool Save();
    }
}
