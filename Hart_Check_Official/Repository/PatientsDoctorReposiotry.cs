using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.EntityFrameworkCore;

namespace Hart_Check_Official.Repository
{
    public class PatientsDoctorReposiotry : IPatientsDoctorRepository
    {
        private readonly datacontext _context;
        public PatientsDoctorReposiotry(datacontext context)
        {
            _context = context;
        }
        public ICollection<PatientsDoctor> GetPatientsDoctor(int patientID)
        {
            return _context.PatientsDoctor.OrderBy(e => e.patientID == patientID).ToList();
        }

        public ICollection<PatientsDoctor> GetPatientsDoctors()
        {
            return _context.PatientsDoctor.OrderBy(p => p.patientDoctorID).ToList();
        }

        public bool PatientsDoctorExist(int patientID)
        {
            return _context.PatientsDoctor.Any(e => e.patientID == patientID);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePatientsDoctort(PatientsDoctor PatientsDoctor)
        {
            _context.Update(PatientsDoctor);
            return Save();
        }
        public PatientsDoctor CreatePatientsDoctor(PatientsDoctor PatientsDoctor)
        {
            _context.Add(PatientsDoctor);
            _context.SaveChanges();
            return (PatientsDoctor);
        }

        public bool DeletepPatientsDoctor(PatientsDoctor PatientsDoctor)
        {
            _context.Remove(PatientsDoctor);
            return Save();
        }

        public List<HealthCareProfessionalName> GetHealthCareProfessionals(int patientID)
        {
            return _context.PatientsDoctor
                .Include(pd => pd.doctor)
                .ThenInclude(d => d.User)
                .Where(pd => pd.patientID == patientID && pd.doctor.User.role == 2)
                .Select(pd => new HealthCareProfessionalName 
                { 
                    Email = pd.doctor.User.email,
                    FirstName = pd.doctor.User.firstName, 
                    LastName = pd.doctor.User.lastName 
                })
                .ToList();
        }

        public List<DoctorInfoDto> GetDoctorsByPatientId(int patientID)
        {
            return _context.PatientsDoctor
           .Where(pd => pd.patientID == patientID)
           .Select(pd => new DoctorInfoDto
           {
               doctorID = pd.doctor.doctorID,
               firstName = pd.doctor.User.firstName,
               lastName = pd.doctor.User.lastName
           })
           .ToList();
        }

        public PatientsDoctor GetPatientsDoctorByEmailAndName(string email, string firstName, string lastName)
        {
            return _context.PatientsDoctor
                        .Include(pd => pd.patient)
                        .ThenInclude(p => p.User)
                        .Include(pd => pd.doctor)
                        .ThenInclude(d => d.User)
                        .FirstOrDefault(pd => pd.patient.User.email == email && pd.doctor.User.firstName == firstName && pd.doctor.User.lastName == lastName);
        }
    }
}
