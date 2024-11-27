using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(User user);
        Task<string> AuthenticateUser(string email, string password);
    }
}
