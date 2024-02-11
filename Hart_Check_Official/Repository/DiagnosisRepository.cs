using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly datacontext _context;
        public DiagnosisRepository(datacontext context)
        {
            _context = context;
        }

        public bool DiagnoseExist(int diagnosisID)
        {
            return _context.Diagnosis.Any(e => e.diagnosisID == diagnosisID);
        }

        public Diagnosis GetDiagnose(int diagnosisID)
        {
            return _context.Diagnosis.Where(e => e.diagnosisID == diagnosisID).FirstOrDefault();
        }

        public ICollection<Diagnosis> GetDiagnoses()
        {
            return _context.Diagnosis.OrderBy(e => e.diagnosisID).ToList();
        }

        public ICollection<Diagnosis> GetDiagnosisByPatientId(int patientID)
        {
            return _context.Diagnosis
                               .Join(_context.Consultation,
                                     diagnosis => diagnosis.consultationID,
                                     consultation => consultation.consultationID,
                                     (diagnosis, consultation) => new { diagnosis, consultation })
                               .Where(x => x.consultation.patientID == patientID)
                               .Select(x => x.diagnosis)
                               .ToList();
        }
    }
}
