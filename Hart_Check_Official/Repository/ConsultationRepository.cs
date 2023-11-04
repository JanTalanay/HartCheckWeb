using AutoMapper;
using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.EntityFrameworkCore;

namespace Hart_Check_Official.Repository
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly datacontext _context;

        private readonly IMapper _mapper;
        public ConsultationRepository(datacontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public Consultation CreateConsultation(Consultation consultation)
        {
            _context.Add(consultation);
            _context.SaveChanges();
            return (consultation);
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

        public DoctorSchedule GetDoctorScheduleByID(int doctorSchedID)
        {
            return _context.DoctorSchedule.FirstOrDefault(ds => ds.doctorSchedID == doctorSchedID);
        }

        public HealthCareProfessional GetHealthCareProfessionalByID(int doctorID)
        {
            return _context.HealthCareProfessional.FirstOrDefault(hcp => hcp.doctorID == doctorID);
        }


        public Users GetDoctorUserByDoctorId(int doctorID)
        {
            var doctor = _context.HealthCareProfessional
                .Include(hcp => hcp.User)
                .FirstOrDefault(hcp => hcp.doctorID == doctorID);

            return doctor?.User;
        }
        public ICollection<DoctorScheduleDto> GetDoctorSchedulesForPatient(int patientID)
        {
            var consultations = _context.Consultation.Where(c => c.patientID == patientID).ToList();
            var doctorSchedules = new List<DoctorScheduleDto>();

            foreach (var consultation in consultations)
            {
                var doctorSchedule = _context.DoctorSchedule.FirstOrDefault(ds => ds.doctorSchedID == consultation.doctorSchedID);
                if (doctorSchedule != null)
                {
                    var doctorScheduleDto = _mapper.Map<DoctorScheduleDto>(doctorSchedule);
                    doctorSchedules.Add(doctorScheduleDto);
                }
            }

            return doctorSchedules;
        }

        public Consultation GetConsultationdoctorSchedID(int doctorSchedID)
        {
            return _context.Consultation.Where(e => e.doctorSchedID == doctorSchedID).FirstOrDefault();
        }

        public bool consultationExistsdoctorSchedID(int doctorSchedID)
        {
            return _context.Consultation.Any(e => e.doctorSchedID == doctorSchedID);
        }
    }
}
