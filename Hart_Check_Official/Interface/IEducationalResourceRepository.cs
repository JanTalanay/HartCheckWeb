using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IEducationalResourceRepository
    {
        ICollection<EducationalResource> GetEducationalResource();
        EducationalResource GetEducationalResources(int resourceID);
        bool EducationalResourceExist(int resourceID);
    }
}
