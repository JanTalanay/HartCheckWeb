using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IBodyMassRepository
    {
        //get the data based on the patientID
        //delete or update based on their original ID
        ICollection<BodyMass> GetBodies();

        BodyMass GetBodyMass(int bodyMassID);
        BodyMass GetBodyMassPatientID(int patientID);

        bool BodyMassExist(int bodyMassID);
        bool BodyMassExistPatientID(int patientID);
        BodyMass CreateBodyMass(BodyMass bodyMass);

        bool UpdateBodyMass(BodyMass bodyMass);

        bool DeleteBodyMass(BodyMass bodyMass);

        bool Save();
    }
}
