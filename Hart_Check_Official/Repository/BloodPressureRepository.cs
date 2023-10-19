using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class BloodPressureRepository : IBloodPressureRepository
    {
        private readonly datacontext _context;
        public BloodPressureRepository(datacontext context)
        {
            _context = context;
        }
        public bool BloodPressureExistPatientID(int patientID)//checks the table based on the id of patientID
        {
            return _context.BloodPressure.Any(e => e.patientID == patientID);
        }
        public bool BloodPressureExist(int bloodPressureID)//checks the table based on the id of blood pressure
        {
            return _context.BloodPressure.Any(e => e.bloodPressureID == bloodPressureID);
        }


        public ICollection<BloodPressure> GetBloodPressPatientID(int patientID)
        {
            return _context.BloodPressure.Where(e => e.patientID == patientID).ToList();
        }
        public ICollection<BloodPressure> GetBloodPressures()
        {
            return _context.BloodPressure.OrderBy(p => p.bloodPressureID).ToList();
        }
        public BloodPressure GetBloodPress(int bloodPressureID)
        {
            return _context.BloodPressure.Where(e => e.bloodPressureID == bloodPressureID).FirstOrDefault();
        }
        //public BloodPressure GetBloodPressPatientID(int patientID)
        //{
        //    return _context.BloodPressure.Where(e => e.patientID == patientID).FirstOrDefault();
        //}

        public bool UpdateBloodPressure(BloodPressure bloodpressure)
        {
            _context.Update(bloodpressure);
            return Save();
        }
        public bool CreateBloodPressure(BloodPressure bloodpressure)
        {
            _context.Add(bloodpressure);
            return Save();
        }

        public bool DeleteBloodPressure(BloodPressure bloodpressure)
        {
            _context.Remove(bloodpressure);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
