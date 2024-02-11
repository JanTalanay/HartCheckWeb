using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IMedicineRepository
    {
        ICollection<Medicine> GetMedicines();
        Medicine GetMedicine(int medicineID);
        bool MedicineExist(int medicineID);
        ICollection<Medicine> GetMedicinesByPatientID(int patientID);
    }
}
