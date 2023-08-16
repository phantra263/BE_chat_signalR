using Chat.Application.Features.Message.Queries.GetByConversationId;
using Chat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IMessageRepositoryAsync
    {
        Task<IList<Message>> GetAsync();
        Task<Message> GetByIdAsync(string id);
        Task<Message> CreateAsync(Message message);
        Task UpdateAsync(string id, Message message);
        Task DeleteAsync(string id);
        Task<IReadOnlyList<Message>> GetMessageChatAsync(int pageNumber, int pageSize, string keyword, string senderId, string receiverId);
        Task<Message> GetLatestMessageChatAsync(string senderId, string receiverId);
        Task<Message> GetLatestMessageByConversation(string conversationId);
        Task<IReadOnlyList<HistoryChatModel>> GetMessageByConversation(int pageNumber, int pageSize, string keyword, string conversationId);
        Task<IReadOnlyList<Message>> GetByConversationId(string conversationId);

        Task UpdateOneById(string id);
        Task UpdateManyByConversation(string conversationId);
    }
}
