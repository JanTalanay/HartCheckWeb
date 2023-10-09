﻿using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class BodyMassRepository : IBodyMassRepository
    {
        private readonly datacontext _context;
        public BodyMassRepository(datacontext context)
        {
            _context = context;
        }

        public bool BodyMassExist(int bodyMassID)
        {
            return _context.BodyMass.Any(e => e.bodyMassID == bodyMassID);
        }

        public bool CreateBodyMass(BodyMass bodyMass)
        {
            _context.Add(bodyMass);
            return Save();
        }

        public Task<bool> CreateBodyMassAsync(BodyMass bodyMass)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBodyMass(BodyMass bodyMass)
        {
            _context.Remove(bodyMass);
            return Save();
        }

        public ICollection<BodyMass> GetBodies()
        {
            return _context.BodyMass.OrderBy(p => p.bodyMassID).ToList();
        }

        public BodyMass GetBodyMass(int bodyMassID)
        {
            return _context.BodyMass.Where(e => e.bodyMassID == bodyMassID).FirstOrDefault();
        }

        public int GetBodyMassByID(int bodyMassID)//Ignore this
        {
            var bodyMass = _context.BodyMass.Where(e => e.bodyMassID == bodyMassID);

            if (bodyMass.Count() <= 0)
            {
                return 0;
            }
            return bodyMass.Count();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBodyMass(BodyMass bodyMass)
        {
            _context.Update(bodyMass);
            return Save();
        }
    }
}