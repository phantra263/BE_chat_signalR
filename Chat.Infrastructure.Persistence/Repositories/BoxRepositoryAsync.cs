using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public class BoxRepositoryAsync : IBoxRepositoryAsync
    {
        private readonly IMongoCollection<Box> _box;

        public BoxRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _box = database.GetCollection<Box>(Collections.BoxCollection);
        }

        public async Task<Box> CreateAsync(Box box)
        {
            await _box.InsertOneAsync(box);
            return box;
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _box.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Box> GetByIdAsync(string id)
        {
            return await _box.Find(x => x.Deleted != true && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<Box>> GetAsync()
        {
            return await _box.Find(x => true).ToListAsync();
        }

        public async Task UpdateAsync(string id, Box box)
        {
            await _box.ReplaceOneAsync(x => x.Id == id, box);
        }

        public async Task<Box> GetCheckExist(string user1Id, string user2Id)
        {
            return await _box.Find(x => x.Deleted != true && x.User1Id == user1Id && x.User2Id == user2Id).FirstOrDefaultAsync();
        }

        public async Task FindAndDeleteByUserAsync(string user1Id, string user2Id)
        {
            await _box.DeleteOneAsync(x => x.Deleted != true && x.User1Id == user1Id && x.User2Id == user2Id);
        }

        // func check user 2 đã tạo hội thoại trước đó chưa
        public async Task<Box> GetCheckUsr2AccessUsr1(string user1Id, string user2Id)
        {
            return await _box.Find(x => x.Deleted != true && x.User1Id == user2Id && x.User2Id == user1Id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Box>> GetBoxsUserChatWith(string userChatWithId)
        {
            return await _box.Find(x => x.Deleted != true && x.User2Id == userChatWithId).ToListAsync();
        }
    }
}
