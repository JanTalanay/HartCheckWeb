using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class HealthCareProfessionalRepository : IHealthCareProfessionalRepository
    {
        private readonly datacontext _context;
        public HealthCareProfessionalRepository(datacontext context)
        {
            _context = context;
        }

        public HealthCareProfessional GetHealthCareProfessional(int doctorID)
        {
            return _context.HealthCareProfessional.Where(e => e.doctorID == doctorID).FirstOrDefault();
        }

        public ICollection<HealthCareProfessional> GetHealthCareProfessionals()
        {
            return _context.HealthCareProfessional.OrderBy(e => e.doctorID).ToList();
        }

        public bool HealthCareProfessionalExist(int doctorID)
        {
            return _context.HealthCareProfessional.Any(e => e.doctorID == doctorID);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateHealthCareProfessional(HealthCareProfessional doctorID)
        {
            _context.Update(doctorID);
            return Save();
        }
        public HealthCareProfessional CreateHealthCareProfessional(HealthCareProfessional doctorID)
        {
            _context.Add(doctorID);
            _context.SaveChanges();
            return (doctorID);
        }

        public bool DeletepHealthCareProfessional(HealthCareProfessional doctorID)
        {
            _context.Remove(doctorID);
            return Save();
        }
    }
}
