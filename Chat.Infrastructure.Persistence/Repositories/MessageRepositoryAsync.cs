﻿using Chat.Application.Features.Message.Queries.GetByConversationId;
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
    public class MessageRepositoryAsync : IMessageRepositoryAsync
    {
        private readonly IMongoCollection<Message> _message;
        private readonly IMongoCollection<User> _user;

        public MessageRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _message = database.GetCollection<Message>(Collections.MessageCollection);
            _user = database.GetCollection<User>(Collections.UserCollection);
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

        public async Task<Message> GetLatestMessageChatAsync(string senderId, string receiverId)
        {
            return await _message
                .Find(x => x.Deleted != true && ((x.SenderId == senderId && x.ReceiverId == receiverId) || (x.SenderId == receiverId && x.ReceiverId == senderId)))
                .SortByDescending(x => x.Created)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<HistoryChatModel>> GetMessageByConversation(int pageNumber, int pageSize, string keyword, string conversationId)
        {
            var results = (from mess in _message.AsQueryable().Where(x => x.Deleted != true && x.ConversationId == conversationId)
                          join sender in _user.AsQueryable() on mess.SenderId equals sender.Id
                          join receiver in _user.AsQueryable() on mess.ReceiverId equals receiver.Id
                          select new HistoryChatModel
                          {
                              Id = mess.Id,
                              Created = mess.Created,
                              Deleted = mess.Deleted,
                              ConversationId = mess.ConversationId,
                              SenderId = mess.SenderId,
                              SenderName = sender.Nickname,
                              ReceiverId = mess.ReceiverId,
                              ReceiverName = receiver.Nickname,
                              Content = mess.Content,
                              IsSeen = mess.IsSeen
                          })
                         .OrderByDescending(x => x.Created)
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();

            return results.OrderBy(x => x.Created).ToList();
        }
    }
}
