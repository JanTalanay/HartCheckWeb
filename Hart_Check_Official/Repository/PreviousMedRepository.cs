using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class PreviousMedRepository : IPreviousMedRepository
    {
        private readonly datacontext _context;

        public PreviousMedRepository(datacontext context)
        {
            _context = context;
        }

        public bool CreatePrevMed(PreviousMedication prevMed)
        {
            _context.Add(prevMed);
            return Save();
        }

        public bool DeletePrevMed(PreviousMedication prevMed)
        {
            _context.Remove(prevMed);
            return Save();
        }

        public ICollection<PreviousMedication> GetPreviousMedications()
        {
            return _context.PreviousMedication.OrderBy(p => p.prevMedID).ToList();
        }

        public PreviousMedication GetPrevMed(int prevMedID)
        {
            return _context.PreviousMedication.Where(e => e.prevMedID == prevMedID).FirstOrDefault();
        }


        public bool PrevMedExists(int prevMedID)
        {
            return _context.PreviousMedication.Any(e => e.prevMedID == prevMedID);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePrevMed(PreviousMedication prevMed)
        {
            _context.Update(prevMed);
            return Save();
        }
    }
}
