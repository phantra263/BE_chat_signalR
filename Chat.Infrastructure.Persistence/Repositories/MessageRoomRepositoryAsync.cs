using Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom;
using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public class MessageRoomRepositoryAsync : IMessageRoomRepositoryAsync
    {
        private readonly IMongoCollection<MessageRoom> _messageRoom;
        private readonly IMongoCollection<User> _user;

        public MessageRoomRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _messageRoom = database.GetCollection<MessageRoom>(Collections.MessageRoomCollection);
            _user = database.GetCollection<User>(Collections.UserCollection);
        }

        public async Task<MessageRoom> GetLatestMessageInRoom(string roomId)
            => await _messageRoom.Find(x => x.Deleted != true && x.Id == roomId).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<HistoryMessageRoomViewModel>> GetMessageInRoom(int pageNumber, int pageSize, string keyword, string roomId)
        {
            var results = (from mess in _messageRoom.AsQueryable().Where(x => x.Deleted != true && x.RoomId == roomId)
                          join user in _user.AsQueryable() on mess.SenderId equals user.Id into lf_us
                          from us in lf_us.DefaultIfEmpty()
                          orderby mess.Created descending
                          select new HistoryMessageRoomViewModel
                          {
                              SenderId = us.Id,
                              AvatarId = us.AvatarId,
                              AnonymousName = us.AnonymousName,
                              Content = mess.Content,
                              Created = mess.Created
                          })
                          .Skip((pageNumber - 1) * pageSize)
                          .Take(pageSize)
                          .ToList();

            return results.OrderBy(x => x.Created).ToList();
        }

        public async Task<MessageRoom> InsertOneAsync(MessageRoom entity)
        {
            await _messageRoom.InsertOneAsync(entity);
            return entity;
        }
    }
}
