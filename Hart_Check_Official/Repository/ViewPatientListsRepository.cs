using Hart_Check_Official.Data;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;

namespace Hart_Check_Official.Repository
{
    public class ViewPatientListsRepository : IViewPatientListsRepository
    {
        private readonly datacontext _context;

        public ViewPatientListsRepository(datacontext context)
        {
            _context = context;
        }

        public ICollection<ViewPatientDto> GetUser()
        {
            // ID of the doctor and his all patients

            return _context.Users
            .OrderBy(p => p.usersID)
            .Select(user => new ViewPatientDto
            {
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                phoneNumber = user.phoneNumber

                //date creation Imma add
            })
            .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExists(int userID)
        {
            return _context.Users.Any(e => e.usersID == userID);
        }

        Users IViewPatientListsRepository.GetUsers(int userID)
        {
            throw new NotImplementedException();
        }
    }
}