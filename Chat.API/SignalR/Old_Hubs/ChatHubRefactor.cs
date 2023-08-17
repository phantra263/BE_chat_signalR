using Chat.API.Models.Requests;
using Chat.Application.Logging;
using Chat.Application.Wrappers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.API.SignalR.Old_Hubs
{
    public class ChatHubRefactor : Hub
    {
        private static readonly Dictionary<string, List<string>> usersOnline = new Dictionary<string, List<string>>();
        private readonly IMemoriesLog _memories;

        public ChatHubRefactor(IMemoriesLog memories)
        {
            _memories = memories;
        }

        public override async Task OnConnectedAsync()
        {
            var beforeResource = _memories.GetResourceMemories("");

            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                if (!usersOnline.ContainsKey(userId))
                    usersOnline.Add(userId, new List<string>());

                usersOnline[userId].Add(Context.ConnectionId);

                await Clients.All.SendAsync("onConnected", new Response<object>(new { UserId = userId, IsOnline = true }));

                await Clients.Client(Context.ConnectionId).SendAsync("OnGetListUserOnline", new Response<object>(new { UserOnline = usersOnline.Keys, IsOnline = true }));
            }

            await base.OnConnectedAsync();

            var afterResource = _memories.GetResourceMemories("");
            Console.WriteLine($"Memory after connection: {afterResource - beforeResource} byte");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var beforeResource = _memories.GetResourceMemories("");

            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId) && usersOnline.ContainsKey(userId))
            {
                var connectionIds = usersOnline[userId];
                connectionIds.Remove(Context.ConnectionId);

                if (connectionIds.Count == 0)
                {
                    usersOnline.Remove(userId);
                    await Clients.All.SendAsync("onDisconnected", new Response<object>(new { UserId = userId, IsOnline = false }));
                }
            }

            await base.OnDisconnectedAsync(exception);

            var afterResource = _memories.GetResourceMemories("");
            Console.WriteLine($"Memory after disconnection: {beforeResource - afterResource} byte");
        }

        public async Task SendMessage(MessageRequest request)
        {
            var beforeResource = _memories.GetResourceMemories("");

            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                foreach (var connectionId in usersOnline[userId])
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", new Response<object>(new { request.ConversationId, SenderId = userId, request.SenderName, request.ReceiverId, request.Content }));
                }

                if (!userId.Equals(request.ReceiverId))
                {
                    int cnt = 0;
                    foreach (var connectionId in usersOnline[request.ReceiverId])
                    {
                        ++cnt;
                        await Clients.Client(connectionId).SendAsync("ReceiveMessage", new Response<object>(new { request.ConversationId, SenderId = userId, request.SenderName, request.ReceiverId, request.Content }));

                        if (cnt == 1)
                            await Clients.Client(connectionId).SendAsync("ReceiveNotificationMessage", new Response<object>(new { Content = $"Bạn có 1 tin nhắn mới từ {request.SenderName}" }));
                    }
                }
            }

            var afterResource = _memories.GetResourceMemories("");
            Console.WriteLine($"Memory after send message: {afterResource - beforeResource} byte");
        }

        public async Task SendMessageV2(MessageRequest request)
        {
            var beforeResource = _memories.GetResourceMemories("");

            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId))
                return;

            async Task SendToConnections(IEnumerable<string> connectionIds)
            {
                foreach (var connectionId in connectionIds)
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", new Response<object>(new { request.ConversationId, SenderId = userId, request.SenderName, request.ReceiverId, request.Content }));
                }
            }

            async Task SendNotification(string connectionId)
            {
                await Clients.Client(connectionId).SendAsync("ReceiveNotificationMessage", new Response<object>(new { Content = $"Bạn có 1 tin nhắn mới từ {request.SenderName}" }));
            }

            if (usersOnline.TryGetValue(userId, out var senderConnections))
            {
                await SendToConnections(senderConnections);

                if (!userId.Equals(request.ReceiverId) && usersOnline.TryGetValue(request.ReceiverId, out var receiverConnections))
                {
                    int cnt = 0;
                    foreach (var connectionId in receiverConnections)
                    {
                        ++cnt;
                        await SendToConnections(new[] { connectionId });

                        if (cnt == 1)
                        {
                            await SendNotification(connectionId);
                        }
                    }
                }
            }

            var afterResource = _memories.GetResourceMemories("");
            Console.WriteLine($"Memory after send message: {afterResource - beforeResource} byte");
        }

    }
}
