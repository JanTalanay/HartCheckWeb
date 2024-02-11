using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IPatientRepository
    {
        ICollection<Patients> GetPatient();

        Patients GetPatients(int usersID);
        string GetEmailByPatientId(int patientId);

        //Patients GetBugReport(string description);
        bool patientExist(int usersID);

        bool Createpatient(Patients patient);
        bool Deletepatient(Patients patient);
        bool Updatepatient(Patients patient);

        bool Save();
    }
}
