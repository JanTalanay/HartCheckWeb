using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly datacontext _context;
        public ConsultationRepository(datacontext context)
        {
            _context = context;
        }
        public ICollection<Consultation> GetConsultations()
        {
            return _context.Consultation.OrderBy(p => p.consultationID).ToList();
        }
        public bool consultationExists(int consultationID)
        {
            return _context.Consultation.Any(e => e.consultationID == consultationID);
        }
        public bool consultationExistsPatientsID(int patientID)
        {
            return _context.Consultation.Any(e => e.patientID == patientID);
        }
        public Consultation GetConsultation(int consultationID)
        {
            return _context.Consultation.Where(e => e.consultationID == consultationID).FirstOrDefault();
        }
        public Consultation GetConsultationPatientsID(int patientID)
        {
            return _context.Consultation.Where(e => e.patientID == patientID).FirstOrDefault();
        }





        public bool UpdateConsultation(Consultation consultation)
        {
            _context.Update(consultation);
            return Save();
        }
        public bool CreateConsultation(Consultation consultation)
        {
            _context.Add(consultation);
            return Save();
        }

        public bool DeleteConsultation(Consultation consultation)
        {
            _context.Remove(consultation);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
