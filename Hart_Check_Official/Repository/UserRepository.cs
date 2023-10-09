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

        public bool CreateUsers(Users users)
        {
            _context.Add(users);
            //int userID = users.usersID;

            //if (users.role == 1)
            //{
            //    var patient = new Patients
            //    {
            //        usersID = userID,
            //    };
            //    _context.Add(users);
            //    _context.Add(patient);
            //}
            //else if (users.role == 2)
            //{
            //    var doctor = new HealthCareProfessional
            //    {
            //        usersID = userID,
            //    };
            //    _context.Add(users);
            //    _context.Add(doctor);
            //}
            return Save();
        }

        public async Task<bool> CreateUsersAsync(Users users)//it works, causing 500 error
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
            return Save();
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

        public bool LoginUsers(Users users)
        {
            var user = new Users();
            if (_context.Users.Any(users => users.email.Equals(users.email)))
            {
                user = _context.Users.Where(user => user.email.Equals(users.email)).First();
            }
            return Save();
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
