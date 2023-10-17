using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class BMITypeRepository : IBMITypeRepository
    {
        private readonly datacontext _context;

        public BMITypeRepository(datacontext context)
        {
            _context = context;
        }
        public BMIType GetBMI(int BMITypeID)
        {
            return _context.BMIType.Where(e => e.BMITypeID == BMITypeID).FirstOrDefault();
        }

        public ICollection<BMIType> GetBMITypes()
        {
            return _context.BMIType.OrderBy(p => p.BMITypeID).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool BMITypeExists(int BMITypeID)
        {
            return _context.BMIType.Any(e => e.BMITypeID == BMITypeID);
        }
    }
}
