using API_User.Authentication.Encryption;
using API_User.Exeptions;
using API_User.Models;
using API_User.Repositories;
using System.ComponentModel.DataAnnotations;

namespace API_User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidations _validations;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IValidations validations, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _validations = validations;
            _logger = logger;

        }
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddUserAsync(User user)
        {
            var users = await _userRepository.GetAllAsync();

            var result = users.FirstOrDefault(x => x.Email == user.Email);

            if (result != null)
            {
                if (result != null)
                {
                    _logger.LogError("This email is already registered in the system.");
                    throw new ValidationException("This email is already registered in the system.");
                }
            }

            user.PassWord = _validations.HashPassword(user.PassWord);
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);

            if (existingUser == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            await _userRepository.DeleteAsync(id);
        }
    }
}
