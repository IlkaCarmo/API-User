using API_User.Models;
using MongoDB.Driver;

namespace API_User.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserRepository(IConfiguration config)
        {
            var mongoSettings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>();
            var client = new MongoClient(mongoSettings.ConnectionString);
            var database = client.GetDatabase(mongoSettings.DatabaseName);
            _usersCollection = database.GetCollection<User>("Users");
        }

        public async Task<User?> GetByIdAsync(string id) =>
        await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<User>> GetAllAsync() =>
        await _usersCollection.Find(user => true).ToListAsync();

        public async Task AddAsync(User user) =>
        await _usersCollection.InsertOneAsync(user);

        public async Task UpdateAsync(User user) =>
        await _usersCollection.ReplaceOneAsync(u => u.Id == user.Id, user);
        public async Task DeleteAsync(string id) =>
        await _usersCollection.DeleteOneAsync(user => user.Id == id);
    }
}
