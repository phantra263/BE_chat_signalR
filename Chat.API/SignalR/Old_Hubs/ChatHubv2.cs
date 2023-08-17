using Chat.API.Models.Requests;
using Chat.Application.Wrappers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.API.SignalR.Old_Hubs
{
    public class ChatHubv2 : Hub
    {
        private static readonly Dictionary<string, List<string>> usersOnline = new Dictionary<string, List<string>>();

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                if (usersOnline.ContainsKey(userId))
                {
                    usersOnline[userId].Add(Context.ConnectionId);
                }
                else
                {
                    usersOnline.Add(userId, new List<string> { Context.ConnectionId });
                }

                await Clients.All.SendAsync("onConnected", new Response<object>(new { UserId = userId, isOnline = true }));

                await Clients.Client(Context.ConnectionId).SendAsync("OnGetListUserOnline", new Response<object>(new { UserOnline = usersOnline.Keys, IsOnline = true }));
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                if (usersOnline.ContainsKey(userId) && usersOnline[userId].Contains(Context.ConnectionId))
                {
                    if (GetConnectionIds(userId).Count > 1)
                    {
                        usersOnline[userId].Remove(Context.ConnectionId);
                    }
                    else
                    {
                        usersOnline.Remove(userId);

                        await Clients.All.SendAsync("onDisconnected", new Response<object>(new { UserId = userId, isOnline = false }));
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public async Task SendMessage(MessageRequest request)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                var response = new Response<object>(new { request.ConversationId, SenderId = userId, request.SenderName, request.ReceiverId, request.Content });

                foreach (var connectionId in GetConnectionIds(userId))
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", response);
                }

                if (!userId.Equals(request.ReceiverId))
                {
                    int cnt = 0;
                    foreach (var connectionId in GetConnectionIds(request.ReceiverId))
                    {
                        ++cnt;
                        await Clients.Client(connectionId).SendAsync("ReceiveMessage", response);

                        // gửi thông báo cho user khi có tin nhắn mới
                        if (cnt == 1)
                            await Clients.Client(connectionId).SendAsync("ReceiveNotificationMessage", new Response<object>(new { Content = $"Bạn có 1 tin nhắn mới từ {request.SenderName}" }));
                    }
                }
            }
        }

        public async Task ReadMessage(ReadMessageRequest request)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                var response = new Response<object>(new { SenderId = userId, request.ReceiverId, Seen = request.IsSeen });

                foreach (var connectionId in GetConnectionIds(userId))
                {
                    await Clients.Client(connectionId).SendAsync("OnReadMessage", response);
                }

                if (!userId.Equals(request.ReceiverId))
                {
                    foreach (var connectionId in GetConnectionIds(request.ReceiverId))
                    {
                        await Clients.Client(connectionId).SendAsync("OnReadMessage", response);
                    }
                }
            }
        }

        private List<string> GetConnectionIds(string key)
        {
            return usersOnline.ContainsKey(key) ? usersOnline[key] : new List<string>();
        }
    }
}
