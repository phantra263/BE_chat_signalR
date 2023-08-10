using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public class MessageRepositoryAsync : IMessageRepositoryAsync
    {
        private readonly IMongoCollection<Message> _message;

        public MessageRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _message = database.GetCollection<Message>(Collections.MessageCollection);
        }

        public async Task<Message> CreateAsync(Message message)
        {
            await _message.InsertOneAsync(message);
            return message;
        }

        public async Task DeleteAsync(string id)
        {
            await _message.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<Message> GetByIdAsync(string id)
        {
            return await _message.Find(x => x.Deleted != true && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<Message>> GetAsync()
        {
            return await _message.Find(x => true).ToListAsync();
        }

        public async Task UpdateAsync(string id, Message message)
        {
            await _message.ReplaceOneAsync(x => x.Id == id, message);
        }

        public async Task<IReadOnlyList<Message>> GetMessageChatAsync(int pageNumber, int pageSize, string keyword, string senderId, string receiverId)
        {
            return await _message
                .Find(x => x.Deleted != true && (x.Content.Contains(keyword) || string.IsNullOrEmpty(keyword)) &&
                     ((x.SenderId == senderId && x.ReceiverId == receiverId) || (x.SenderId == receiverId && x.ReceiverId == senderId)))
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }
    }
}
