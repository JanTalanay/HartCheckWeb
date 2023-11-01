using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.EntityFrameworkCore;

namespace Hart_Check_Official.Repository
{
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly datacontext _context;
        public DoctorScheduleRepository(datacontext context)
        {
            _context = context;
        }
        public bool DoctorScheduleExist(int doctorID)
        {
            return _context.DoctorSchedule.Any(e => e.doctorID == doctorID);
        }

        public DoctorSchedule GetDoctorSchedule(int doctorID)
        {
            return _context.DoctorSchedule.Where(e => e.doctorID == doctorID).FirstOrDefault();
        }

        public ICollection<DoctorSchedule> GetDoctorSchedules()
        {
            return _context.DoctorSchedule.OrderBy(e => e.doctorSchedID).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDoctorSchedule(DoctorSchedule doctorSchedID)
        {
            _context.Update(doctorSchedID);
            return Save();
        }
        public DoctorSchedule CreateDoctorSchedule(DoctorSchedule doctorSchedID)
        {
            _context.Add(doctorSchedID);
            _context.SaveChanges();
            return (doctorSchedID);
        }

        public bool DeletepDoctorSchedule(DoctorSchedule doctorSchedID)
        {
            _context.Remove(doctorSchedID);
            return Save();
        }

        public async Task<(HealthCareProfessional, HealthCareProfessionalName)> GetDoctorDetails(int patientID)
        {
            var doctorDetails = await _context.PatientsDoctor
                  .Where(pd => pd.patientID == patientID)
                  .Include(pd => pd.doctor)
                      .ThenInclude(d => d.User)
                  .FirstOrDefaultAsync();

            var doctorName = new HealthCareProfessionalName
            {
                FirstName = doctorDetails.doctor.User.firstName,
                LastName = doctorDetails.doctor.User.lastName
            };

            return (doctorDetails.doctor, doctorName);
        }

        public async Task<List<DateTime>> GetDoctorSched(int doctorID)
        {
            var doctorSchedule = await _context.DoctorSchedule
                .Where(ds => ds.doctorID == doctorID)
                .Select(ds => ds.schedDateTime)
                .ToListAsync();

            return doctorSchedule;
        }

        public async Task<List<(HealthCareProfessional, HealthCareProfessionalName, List<DateTime>)>> GetDoctorsDetailsAndSchedules(int patientID)
        {
            var doctorDetailsAndSchedules = new List<(HealthCareProfessional, HealthCareProfessionalName, List<DateTime>)>();

            var patientDoctors = await _context.PatientsDoctor
                .Where(pd => pd.patientID == patientID)
                .Include(pd => pd.doctor)
                    .ThenInclude(d => d.User)
                .ToListAsync();

            foreach (var patientDoctor in patientDoctors)
            {
                var doctorName = new HealthCareProfessionalName
                {
                    FirstName = patientDoctor.doctor.User.firstName,
                    LastName = patientDoctor.doctor.User.lastName
                };

                var doctorSchedule = await _context.DoctorSchedule
                    .Where(ds => ds.doctorID == patientDoctor.doctorID)
                    .Select(ds => ds.schedDateTime)
                    .ToListAsync();

                doctorDetailsAndSchedules.Add((patientDoctor.doctor, doctorName, doctorSchedule));
            }

            return doctorDetailsAndSchedules;
        }
        public bool PatientDoctorExist(int patientID)
        {
            return _context.PatientsDoctor.Any(pd => pd.patientID == patientID);
        }

        public async Task<Dictionary<int, List<DateTime>>> GetDoctorSchedulesForPatient(int patientID)
        {
            var patientDoctors = await _context.PatientsDoctor
                                  .Include(pd => pd.doctor)
                                  .ThenInclude(d => d.DoctorSchedule)
                                  .Where(pd => pd.patientID == patientID)
                                  .ToListAsync();

            var datesByDoctor = new Dictionary<int, List<DateTime>>();

            foreach (var patientDoctor in patientDoctors)
            {
                var doctorSchedules = patientDoctor.doctor.DoctorSchedule;
                var dates = doctorSchedules.Select(ds => ds.schedDateTime).ToList();

                datesByDoctor.Add(patientDoctor.doctorID, dates);
            }

            return datesByDoctor;
        }
    }
}
