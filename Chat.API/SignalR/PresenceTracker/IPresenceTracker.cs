using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.API.SignalR.PresenceTracker
{
    public interface IPresenceTracker
    {
        Task<bool> UserConnected(string userId, string connectionId);
        Task<bool> UserDisconnected(string userId, string connectionId);
        List<string> GetConnectionIds(string userId);
        Task<Dictionary<string, List<string>>> GetUserOnlines();
    }
}
