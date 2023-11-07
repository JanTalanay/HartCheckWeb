using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.EntityFrameworkCore;

namespace Hart_Check_Official.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly datacontext _context;

        public UserRepository(datacontext context)
        {
            _context = context;
        }

        public ICollection<Users> GetUser()
        {
            return _context.Users.OrderBy(p => p.usersID).ToList();
        }

        public Users GetUsers(int userID)
        {
            return _context.Users.Where(e => e.usersID == userID).FirstOrDefault();
        }

        public Users GetUsersEmail(string email)
        {
            return _context.Users.Where(e => e.email == email).FirstOrDefault();
        }

        public Users LoginUsers(Login login)
        {
            var user = _context.Users.SingleOrDefault(x => x.email == login.email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.password, user.password))
            {
                throw new Exception("Invalid email or password.");
            }

            return user;
        }

        public bool CreateUsers(Users users)
        {
            users.password = BCrypt.Net.BCrypt.HashPassword(users.password);
            _context.Add(users);
            return Save();
        }

        public async Task<Users> CreateUsersAsync(Users users)
        {
            try
            {
                users.password = BCrypt.Net.BCrypt.HashPassword(users.password);
                _context.Add(users);
                await _context.SaveChangesAsync();

                int userID = users.usersID;

                if (users.role == 1)
                {
                    var patient = new Patients
                    {
                        usersID = userID,
                    };
                    _context.Add(patient);
                    await _context.SaveChangesAsync();
                }
                else if (users.role == 2)
                {
                    var doctor = new HealthCareProfessional
                    {
                        usersID = userID,
                        licenseID = null,
                        clinic = null,
                        verification = null,
                    };
                    _context.Add(doctor);
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();

                return (users);
            }
            catch (Exception ex)
            {
                //Log the exception details
                return null;
            }
        }

        public bool UpdateUsers(Users users)
        {

            _context.Update(users);
            return Save();
        }

        public bool UserExists(int userID)
        {
            return _context.Users.Any(e => e.usersID == userID);
        }

        public bool UserExistsEmail(string email)
        {
            return _context.Users.Any(e => e.email == email);
        }
        public bool DeleteUser(Users users)
        {
            var patient = _context.Patients.Include(p => p.BodyMass)
                     .Include(p => p.BloodPressureThreshold)
                     .Include(p => p.MedicalConditions)
                     .Include(p => p.PreviousMedication)
                     .Include(p => p.MedicalHistory)
                     .Include(p => p.BloodPressure)
                     .Include(p => p.Consultation)
                     .Include(p => p.archievedrecord)
                     .Include(p => p.patientDoctor)
                     .FirstOrDefault(p => p.usersID == users.usersID);

            if (patient != null)
            {
                // Get the Doctors associated with the Patient
                var doctors = _context.HealthCareProfessional
                    .Include(d => d.patientDoctor)
                    .Where(d => d.patientDoctor.Any(pd => pd.patientID == patient.patientID))
                    .ToList();

                // Remove associations between the Patient and the Doctors
                foreach (var doctor in doctors)
                {
                    var patientDoctor = doctor.patientDoctor.FirstOrDefault(pd => pd.patientID == patient.patientID);
                    if (patientDoctor != null)
                    {
                        _context.PatientsDoctor.Remove(patientDoctor);
                    }
                }

                _context.Patients.Remove(patient);
            }

            // Load the user and its related entities
            var user = _context.Users
                .Include(u => u.patients)
                .Include(u => u.doctor)
                .FirstOrDefault(u => u.usersID == users.usersID);

            if (user == null)
            {
                return false;
            }


            foreach (var doctor in user.doctor)
            {
                _context.HealthCareProfessional.Remove(doctor);
            }

            // Remove the user
            _context.Users.Remove(user);

            // Save changes
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
