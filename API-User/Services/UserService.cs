using API_User.Models;
using API_User.Repositories;

namespace API_User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentException("O nome do usuário é obrigatório.");

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
