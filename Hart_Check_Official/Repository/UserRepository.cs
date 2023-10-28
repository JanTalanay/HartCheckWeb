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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUsers(Users users)
        {

            _context.Update(users);
            return Save();
        }

        public bool CreateUsers(Users users)
        {
            users.password = BCrypt.Net.BCrypt.HashPassword(users.password);
            _context.Add(users);
            //_context.SaveChanges();

            //if (users.role == 1)
            //{
            //    var patient = new Patients
            //    {
            //        usersID = users.usersID,
            //    };
            //    _context.Add(patient.usersID);
            //    _context.SaveChanges();
            //}
            //else if (users.role == 2)
            //{
            //    var doctor = new HealthCareProfessional
            //    {
            //        usersID = users.usersID,
            //        licenseID = null,
            //        clinic = null,
            //        verification = null,
            //    };
            //    _context.Add(doctor);
            //    _context.SaveChanges();
            //}
            //return users;
            return Save();
        }

        public async Task<Users> CreateUsersAsync(Users users)//it works, causing 500 error
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

        public bool DeleteUser(Users users)
        {
            //_context.Remove(users);
            //return Save();
            // Check if the user is a patient and remove the patient record if it exists
            var patient = _context.Patients.Include(p => p.BodyMass)
                                               .Include(p => p.BloodPressureThreshold)
                                               .Include(p => p.MedicalConditions)
                                               .Include(p => p.PreviousMedication)
                                               .Include(p => p.MedicalHistory)
                                               .Include(p => p.BloodPressure)
                                               .Include(p => p.Consultation)
                                               .Include(p => p.archievedrecord)
                                               .FirstOrDefault(p => p.usersID == users.usersID);
            if (patient != null)
            {
                // Remove associated records
                if (patient.BodyMass != null)
                {
                    _context.BodyMass.RemoveRange(patient.BodyMass);
                }
                if (patient.BloodPressureThreshold != null)
                {
                    _context.BloodPressureThreshold.Remove(patient.BloodPressureThreshold);
                }
                if (patient.MedicalConditions != null)
                {
                    _context.MedicalCondition.RemoveRange(patient.MedicalConditions);
                }
                if (patient.PreviousMedication != null)
                {
                    _context.PreviousMedication.RemoveRange(patient.PreviousMedication);
                }
                if (patient.MedicalHistory != null)
                {
                    _context.MedicalHistory.RemoveRange(patient.MedicalHistory);
                }
                if (patient.BloodPressure != null)
                {
                    _context.BloodPressure.RemoveRange(patient.BloodPressure);
                }
                if (patient.Consultation != null)
                {
                    _context.Consultation.RemoveRange(patient.Consultation);
                }
                if (patient.archievedrecord != null)
                {
                    _context.ArchievedRecord.RemoveRange(patient.archievedrecord);
                }

                // Remove the patient record
                _context.Patients.Remove(patient);
            }

            // Check if the user is a doctor and remove the doctor record if it exists
            var doctor = _context.HealthCareProfessional.FirstOrDefault(d => d.usersID == users.usersID);
            if (doctor != null)
            {
                _context.HealthCareProfessional.Remove(doctor);
            }

            // Finally, remove the user
            _context.Users.Remove(users);

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
    }
}
