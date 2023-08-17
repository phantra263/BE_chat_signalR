using Chat.Domain.Entities;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces.IRepositories
{
    public interface IMessageRoomRepositoryAsync
    {
        Task<MessageRoom> GetLatestMessageInRoom(string roomId);
    }
}
