using Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom;
using Chat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IMessageRoomRepositoryAsync
    {
        Task<MessageRoom> InsertOneAsync(MessageRoom entity);
        Task<IReadOnlyList<HistoryMessageRoomViewModel>> GetMessageInRoom(int pageNumber, int pageSize, string keyword, string roomId);
    }
}
