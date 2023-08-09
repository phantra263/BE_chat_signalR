using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public class UserRepositoryAsync : IUserRepositoryAsync
    {
        private readonly IMongoCollection<User> _user;

        public UserRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(Collections.UserCollection);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _user.InsertOneAsync(user);
            return user;
        }

        public async Task DeleteAsync(string id)
        {
            await _user.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<User> GetByNickNameAsync(string nickname)
        {
            return await _user.Find(x => x.Nickname == nickname).FirstOrDefaultAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _user.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<User>> GetAsync()
        {
            return await _user.Find(x => true).ToListAsync();
        }

        public async Task UpdateAsync(string id, User user)
        {
            await _user.ReplaceOneAsync(x => x.Id == id, user);
        }
    }
}
