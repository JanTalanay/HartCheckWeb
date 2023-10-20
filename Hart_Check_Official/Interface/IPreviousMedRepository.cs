using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IPreviousMedRepository
    {
        ICollection<PreviousMedication>GetPreviousMedications();

        PreviousMedication GetPrevMed(int prevMedID);
        PreviousMedication GetPrevMedPatientID(int patientID);

        bool PrevMedExists(int prevMedID);
        bool PrevMedExistsPatientID(int patientID);
        bool CreatePrevMed(PreviousMedication prevMed);
        bool UpdatePrevMed(PreviousMedication prevMed);

        bool DeletePrevMed(PreviousMedication prevMed);
        bool Save();
    }
}
