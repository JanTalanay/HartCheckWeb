using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class ConditionRepository : IConditionRepository
    {
        private readonly datacontext _context;
        public ConditionRepository(datacontext context)
        {
            _context = context;
        }

        public Condition GetCondition(int conditionID)
        {
            return _context.Condition.Where(e => e.conditionID == conditionID).FirstOrDefault();
        }

        public ICollection<Condition> GetConditions()
        {
            return _context.Condition.OrderBy(e => e.conditionID).ToList();
        }

        public bool ConditionExist(int conditionID)
        {
            return _context.Condition.Any(e => e.conditionID == conditionID);
        }
        public ICollection<Condition> GetConditionsByPatientId(int patientID, int doctorID)
        {
            return _context.Condition
                .Join(_context.Consultation,
                    condition => condition.consultationID,
                    consultation => consultation.consultationID,
                    (condition, consultation) => new { condition, consultation })
                .Join(_context.DoctorSchedule,
                    conditionConsultation => conditionConsultation.consultation.doctorSchedID,
                    doctorSchedule => doctorSchedule.doctorSchedID,
                    (conditionConsultation, doctorSchedule) => new { conditionConsultation.condition, conditionConsultation.consultation, doctorSchedule })
                .Where(x => x.consultation.patientID == patientID && x.doctorSchedule.doctorID == doctorID)
                .Select(x => x.condition)
                .ToList();
        }
    }
}
