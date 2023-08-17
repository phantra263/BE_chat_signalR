using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public class MessageRoomRepositoryAsync : IMessageRoomRepositoryAsync
    {
        private readonly IMongoCollection<MessageRoom> _messageRoom;

        public MessageRoomRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _messageRoom = database.GetCollection<MessageRoom>(Collections.MessageRoomCollection);
        }

        public async Task<MessageRoom> GetLatestMessageInRoom(string roomId)
            => await _messageRoom.Find(x => x.Deleted != true && x.Id == roomId).FirstOrDefaultAsync();
    }
}
