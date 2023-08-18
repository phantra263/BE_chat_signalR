using Chat.Application.Features.Room.Queries.FindAnyAndGetLatestMessage;
using Chat.Application.Features.Room.Queries.FindOneAndGetLatestMessage;
using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public class RoomRepositoryAsync : IRoomRepositoryAsync
    {
        private readonly IMongoCollection<Room> _room;
        private readonly IMongoCollection<MessageRoom> _messageRoom;

        public RoomRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _room = database.GetCollection<Room>(Collections.RoomCollection);
            _messageRoom = database.GetCollection<MessageRoom>(Collections.MessageRoomCollection);
        }

        public async Task<Room> InsertOneAsync(Room room)
        {
            await _room.InsertOneAsync(room);
            return room;
        }

        public async Task<Room> FindOneByIdAsync(string roomId)
            => await _room.Find(x => x.Deleted != true && x.Id == roomId).FirstOrDefaultAsync();

        public async Task<Room> FindOneByNameAsync(string name)
            => await _room.Find(x => x.Deleted != true && x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();

        public async Task<FindOneAndGetLatestMessageViewModel> FindOneAndGetLatestMessage(string roomId)
        {
            var room = await _room.Find(x => x.Deleted != true && x.Id == roomId).FirstOrDefaultAsync();
            var latestMessage = await _messageRoom.Find(x => x.Deleted != true && x.RoomId == roomId).SortByDescending(x => x.Created).FirstOrDefaultAsync();
            return new FindOneAndGetLatestMessageViewModel
            {
                Id = roomId,
                Name = room?.Name,
                Status = room?.Status,
                UserId = room?.UserId,
                Content = latestMessage?.Content,
                SenderId = latestMessage?.SenderId,
                Created = latestMessage?.Created
            };
        }

        public async Task<IList<FindAnyAndGetLatestMessageViewModel>> FindAnyAndGetLatestMessage(int pageNumber, int pageSize, string keyword)
        {
            var results = new List<FindAnyAndGetLatestMessageViewModel>();
            var rooms = await _room.Find(x => x.Deleted != true && (x.Name.Contains(keyword) || string.IsNullOrEmpty(keyword))).ToListAsync();
            if (rooms.Count > 0)
            {
                foreach (var room in rooms.OrderByDescending(x => x.Created).Skip((pageNumber - 1) * pageSize).Take(pageSize))
                {
                    var messages = await _messageRoom.Find(x => x.Deleted != true && x.RoomId == room.Id).ToListAsync();
                    var latestMessage = messages.OrderByDescending(x => x.Created).FirstOrDefault();
                    results.Add(new FindAnyAndGetLatestMessageViewModel
                    {
                        id = room.Id,
                        name = room.Name,
                        status = room?.Status,
                        userId = room.UserId,
                        content = latestMessage?.Content,
                        senderId = latestMessage?.SenderId,
                        created = latestMessage?.Created
                    });
                }
            }
            return results;

            // c2
            //var results = (
            //                from room in _room.AsQueryable()
            //                where room.Deleted != true
            //                join message in _messageRoom.AsQueryable() on room.Id equals message.RoomId into roomMessages
            //                from mess in roomMessages.DefaultIfEmpty()
            //                select new FindAnyAndGetLatestMessageViewModel
            //                {
            //                    id = room.Id,
            //                    name = room.Name,
            //                    status = room.Status,
            //                    userId = room.UserId,
            //                    content = mess.Content,
            //                    senderId = mess.SenderId,
            //                    created = mess.Created
            //                }
            //            );

            //return results.ToList();
        }
    }
}
