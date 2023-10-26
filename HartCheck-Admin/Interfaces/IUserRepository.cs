using HartCheck_Admin.Models;

namespace HartCheck_Admin.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByIdAsyncNoTracking(int id);

        bool Add(User user);
        bool Update(User user);  
        bool Delete(User user);
        bool Save();
        Task<IEnumerable<User>> GetUsersWithIds(IEnumerable<int> userIds);
    }
}
