using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.API.SignalR.PresenceTracker
{
    public class PresenceTracker : IPresenceTracker
    {
        private static readonly Dictionary<string, List<string>> usersOnline = new Dictionary<string, List<string>>();

        public Task<bool> UserConnected(string userId, string connectionId)
        {
            // lấy khóa usersOnline làm key cho thread
            // khi key được cập nhật, lock tạo ra 1 thread độc lập cho luồng xử lý này
            // không để xảy ra xung đột
            // những luồng khác phải chờ cho đến khi thread được xử lý xong và giải phóng key
            lock (usersOnline)
            {
                if (usersOnline.ContainsKey(userId))
                {
                    usersOnline[userId].Add(connectionId);
                }
                else
                {
                    usersOnline.Add(userId, new List<string> { connectionId });
                }
            }

            return Task.FromResult(true);
        }

        public Task<bool> UserDisconnected(string userId, string connectionId)
        {
            bool isOffline = false;

            lock (usersOnline)
            {
                if (!usersOnline.ContainsKey(userId)) return Task.FromResult(isOffline);

                usersOnline[userId].Remove(connectionId);

                if (usersOnline[userId].Count == 0)
                {
                    usersOnline.Remove(userId);

                    isOffline = true;
                }
            }

            return Task.FromResult(isOffline);
        }

        public Task<List<string>> GetConnectionIds(string userId)
        {
            return Task.FromResult(usersOnline.ContainsKey(userId) ? usersOnline[userId] : new List<string>());
        }

        public async Task<Dictionary<string, List<string>>> GetUserOnlines()
        {
            return usersOnline;
        }
    }
}
