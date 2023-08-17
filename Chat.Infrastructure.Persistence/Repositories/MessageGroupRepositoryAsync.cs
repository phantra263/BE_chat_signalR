using Chat.Application.Interfaces.IRepositories;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Infrastructure.Persistence.Repositories
{
    public class MessageGroupRepositoryAsync : IMessageGroupRepositoryAsync
    {
        private readonly IMongoCollection<MessageGroup> _messageGroup;

        public MessageGroupRepositoryAsync(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _messageGroup = database.GetCollection<MessageGroup>(Collections.MessageGroupCollection);
        }
    }
}
