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

        public bool PatientDoctorExist(int patientID)
        {
            return _context.PatientsDoctor.Any(pd => pd.patientID == patientID);
        }

        public List<DoctorSchedule> GetDoctorSchedulesForPatient(int patientID)
        {
            // Get all the doctorSchedID from the Consultation table that are already booked
            var bookedDoctorSchedIDs = _context.Consultation
                                             .Where(c => c.patientID == patientID)
                                             .Select(c => c.doctorSchedID)
                                             .ToList();

            var patientDoctors = _context.PatientsDoctor
                                       .Include(pd => pd.doctor)
                                         .ThenInclude(d => d.DoctorSchedule)
                                       .Where(pd => pd.patientID == patientID)
                                       .ToList();

            var doctorSchedules = new List<DoctorSchedule>();

            foreach (var patientDoctor in patientDoctors)
            {
                if (patientDoctor.doctor != null && patientDoctor.doctor.DoctorSchedule != null)
                {
                    // Add only those schedules that are not already booked
                    doctorSchedules.AddRange(patientDoctor.doctor.DoctorSchedule
                                                       .Where(ds => !bookedDoctorSchedIDs.Contains(ds.doctorSchedID)));
                }
            }

            return doctorSchedules;
        }


    }
}
