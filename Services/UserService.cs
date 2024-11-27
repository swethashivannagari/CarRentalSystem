using CarRentalSystem.Models;
using CarRentalSystem.Repositories;

namespace CarRentalSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenService _jwtTokenService;

        public UserService(IUserRepository userRepository, JwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        // Registers a new user
        public async Task<bool> RegisterUser(User user)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            // Hash the user's password before saving
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Add the user to the database
            await _userRepository.AddUser(user);
            return true;
        }

        // Authenticates a user and returns a JWT token
        public async Task<string> AuthenticateUser(string email, string password)
        {
            // Retrieve the user by email
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null || !VerifyPassword(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Generate a JWT token
            var token = _jwtTokenService.GenerateToken(user.Id,user.Name, user.Email, user.Role);
            return token;
        }

        // Verifies a plaintext password against a hashed password
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
