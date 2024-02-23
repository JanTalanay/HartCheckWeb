using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using System;

namespace Hart_Check_Official.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly datacontext _context;
        public PatientRepository(datacontext context)
        {
            _context = context;
        }
        public bool Createpatient(Patients patient)
        {
            _context.Add(patient);
            return Save();
        }

        public bool Deletepatient(Patients patient)// to be edited
        {
            _context.Remove(patient);
            return Save();
        }

        public Patients GetPatients(int usersID)
        {
            return _context.Patients.Where(e => e.usersID == usersID).FirstOrDefault();
        }

        public ICollection<Patients> GetPatient()
        {
            return _context.Patients.OrderBy(e => e.patientID).ToList();
        }

        public bool patientExist(int usersID)
        {
            return _context.Patients.Any(e => e.usersID == usersID);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Updatepatient(Patients patient)
        {
            _context.Update(patient);
            return Save();
        }
        public string GetEmailByPatientId(int patientId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.patientID == patientId);

            if (patient == null)
            {
                throw new Exception("Patient not found");
            }

            var user = _context.Users.FirstOrDefault(u => u.usersID == patient.usersID);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user.email;
        }

    }
}
