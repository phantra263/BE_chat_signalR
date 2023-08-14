using Chat.Application.Features.Box.Queries.GetBoxChatByUserId;
using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Bson;
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
        private readonly IMongoCollection<Message> _message;
        private readonly IMongoCollection<User> _user;

        public BoxRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _box = database.GetCollection<Box>(Collections.BoxCollection);
            _message = database.GetCollection<Message>(Collections.MessageCollection);
            _user = database.GetCollection<User>(Collections.UserCollection);
        }

        public async Task<Box> CreateAsync(Box box)
        {
            await _box.InsertOneAsync(box);
            return box;
        }

        public async Task DeleteByIdAsync(string id)
            => await _box.DeleteOneAsync(x => x.Id == id);

        public async Task<Box> GetByIdAsync(string id)
            => await _box.Find(x => x.Deleted != true && x.Id == id).FirstOrDefaultAsync();

        public async Task<IList<Box>> GetAsync()
            => await _box.Find(x => true).ToListAsync();

        public async Task UpdateAsync(string id, Box box)
            => await _box.ReplaceOneAsync(x => x.Id == id, box);

        public async Task<Box> GetCheckExist(string user1Id, string user2Id)
            => await _box.Find(x => x.Deleted != true && x.User1Id == user1Id && x.User2Id == user2Id).FirstOrDefaultAsync();

        public async Task FindAndDeleteByUserAsync(string user1Id, string user2Id)
            => await _box.DeleteOneAsync(x => x.Deleted != true && x.User1Id == user1Id && x.User2Id == user2Id);

        // func check user 2 đã tạo hội thoại trước đó chưa
        public async Task<Box> GetCheckUsr2AccessUsr1(string user1Id, string user2Id)
            => await _box.Find(x => x.Deleted != true && x.User1Id == user2Id && x.User2Id == user1Id).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<Box>> GetBoxsUserChatWith(string userChatWithId)
            => await _box.Find(x => x.Deleted != true && x.User2Id == userChatWithId).ToListAsync();

        public async Task<IReadOnlyList<GetBoxChatByUserIdViewModel>> GetBoxChatByUserId(string userId)
        {
            //var lastMessage = await _message.Find(x => x.Deleted != true && (x.SenderId == userId || x.ReceiverId == userId)).ToListAsync();
            //var results =
            //    (from b in _box.AsQueryable().Where(x => x.User1Id == userId)
            //     from mes in lastMessage.AsQueryable().Where(x => (b.User1Id == x.SenderId && b.User2Id == x.ReceiverId) ||
            //                                                      (b.User1Id == x.ReceiverId && b.User2Id == x.SenderId))
            //                                          .OrderByDescending(x => x.Created)
            //                                          .Take(1)
            //                                          .DefaultIfEmpty()
            //     join us in _user.AsQueryable() on b.User2Id equals us.Id into lf_us
            //     from us in lf_us.DefaultIfEmpty()
            //     select new GetBoxChatByUserIdViewModel
            //     {
            //         Id = b.Id,
            //         ConversationId = b.ConversationId,
            //         IsLock = b.IsLock,
            //         IsMute = b.IsMute,
            //         UserId = b.User2Id,
            //         Nickname = us.Nickname,
            //         AvatarBgColor = us.AvatarBgColor,
            //         Status = us.Status,
            //         IsOnline = us.IsOnline,
            //         Content = mes.Content,
            //         IsSeen = mes.IsSeen
            //     }).ToList();

            //return results;

            // basic
            //var docs = _box.Aggregate()
            //         .Lookup("Users"
            //         , "User1Id"
            //         , "_id"
            //         , "asUsers")
            //         .As<BsonDocument>()
            //         .ToList();

            var docs = _box.Aggregate()
                     .Lookup(_user.CollectionNamespace.CollectionName
                         , "Users.User1Id"
                         , "_id"
                         , "asUsers")
                     .As<BsonDocument>()
                     .ToList();

            foreach (var doc in docs)
            {
                Console.WriteLine(doc.ToJson());
            }

            return new List<GetBoxChatByUserIdViewModel>();
        }
    }
}
