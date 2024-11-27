using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface IUserRepository
    {
        public Task AddUser(User user);
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserById(int id);
    }
}
