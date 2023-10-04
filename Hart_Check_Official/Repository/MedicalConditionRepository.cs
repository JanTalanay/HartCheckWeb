using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class MedicalConditionRepository : IMedicalConditionRepository
    {
        private readonly datacontext _context;

        public MedicalConditionRepository(datacontext context)
        {
            _context = context;
        }

        public bool CreateMedicalCondition(MedicalCondition medicalCondition)
        {
            _context.Add(medicalCondition);
            return Save();
        }

        public Task<bool> CreateUsersAsync(MedicalCondition medicalCondition)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMedicalCondition(MedicalCondition medicalCondition)
        {
            _context.Remove(medicalCondition);
            return Save();
        }

        public MedicalCondition GetMedicalCondition(int medCondID)
        {
            return _context.MedicalCondition.Where(e => e.medCondID == medCondID).FirstOrDefault();
        }

        public int GetMedicalConditionID(int medCondID)
        {
            throw new NotImplementedException();
        }

        public ICollection<MedicalCondition> GetMedicalConditions()
        {
            return _context.MedicalCondition.OrderBy(p => p.medCondID).ToList();
        }

        public bool MedicalConditionExists(int medCondID)
        {
            return _context.MedicalCondition.Any(e => e.medCondID == medCondID);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMedicalCondition(MedicalCondition medicalCondition)
        {
            _context.Update(medicalCondition);
            return Save();
        }
    }
}
