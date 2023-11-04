using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class BloodPressureThresholdRepository : IBloodPressureThresholdRepository
    {
        private readonly datacontext _context;
        public BloodPressureThresholdRepository(datacontext context)
        {
            _context = context;
        }
        public BloodPressureThreshold GetBloodPressureThreshold(int patientID)
        {
            return _context.BloodPressureThreshold.Where(e => e.patientID == patientID).FirstOrDefault();
        }

        public bool PatientExists(int patientID)
        {
            return _context.BloodPressureThreshold.Any(e => e.patientID == patientID);
        }

    }
}
