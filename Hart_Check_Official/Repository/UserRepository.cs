﻿using Hart_Check_Official.Data;
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

        public Users CreateUsers(Users users)
        {
            _context.Add(users);
            _context.SaveChanges();

            if (users.role == 1)
            {
                var patient = new Patients
                {
                    usersID = users.usersID,
                };
                _context.Add(patient.usersID);
                _context.SaveChanges();
            }
            else if (users.role == 2)
            {
                var doctor = new HealthCareProfessional
                {
                    usersID = users.usersID,
                    licenseID = null,
                    clinic = null,
                    verification = null,
                };
                _context.Add(doctor);
                _context.SaveChanges();
            }
            return users;

        }

        public async Task<Users> CreateUsersAsync(Users users)//it works, causing 500 error
        {
            try
            {
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
                // Log the exception details
                return null;
            }
        }

        public bool DeleteUser(Users users)
        {
            _context.Remove(users);
            return Save();
        }

        public ICollection<Users> GetUser()
        {
            return _context.Users.OrderBy(p => p.usersID).ToList();
        }

        public Users GetUsers(int userID)
        {
            return _context.Users.Where(e => e.usersID == userID).FirstOrDefault();
        }

        public Users LoginUsers(Login login)
        {
            var user = _context.Users.SingleOrDefault(x => x.email == login.email);
            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }

            //Users pass = _context.Users.Where(Users => pass.email == login.email.Equals(login.password)).First();

            //if(login.password != password.password)
            //{
            //    throw new Exception("Invalid email or password.");
            //}

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

        public bool UserExists(int userID)
        {
            return _context.Users.Any(e => e.usersID == userID);
        }
    }
}
