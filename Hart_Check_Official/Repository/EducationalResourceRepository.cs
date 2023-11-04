using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using System;

namespace Hart_Check_Official.Repository
{
    public class EducationalResourceRepository : IEducationalResourceRepository
    {
        private readonly datacontext _context;
        public EducationalResourceRepository(datacontext context)
        {
            _context = context;
        }
        public bool EducationalResourceExist(int resourceID)
        {
            return _context.EducationalResource.Any(e => e.resourceID == resourceID);
        }

        public ICollection<EducationalResource> GetEducationalResource()
        {
            return _context.EducationalResource.OrderBy(e => e.resourceID).ToList();
        }

        public EducationalResource GetEducationalResources(int resourceID)
        {
            return _context.EducationalResource.Where(e => e.resourceID == resourceID).FirstOrDefault();

        }
    }
}
