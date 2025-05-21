using API_User.Authentication.Encryption;
using API_User.Models;
using API_User.Repositories;

namespace API_User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidations _validations;


        public UserService(IUserRepository userRepository, IValidations validations)
        {
            _userRepository = userRepository;
            _validations = validations;
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
            var result = _userRepository.GetAllAsync().Result.FirstOrDefault(x => x.Email == user.Email);

            if(result.Email == user.Email)
            {
                throw new ArgumentException("This email is already registered in the systems.");
            }

            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException("The username is mandatory.");


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
