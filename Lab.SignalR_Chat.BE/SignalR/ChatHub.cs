using Lab.SignalR_Chat.BE.Models;
using Lab.SignalR_Chat.BE.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab.SignalR_Chat.BE.SignalR
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, List<string>> usersOnline = new Dictionary<string, List<string>>();

        public override async Task OnConnectedAsync()
        {
            // lấy  ra userId được gửi lên
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            // kiểm tra userId có null không?
            if (!string.IsNullOrEmpty(userId))
            {
                // kiểm tra user đã được connect trước đó chưa
                if (usersOnline.ContainsKey(userId))
                {
                    // nếu "rồi" thì add thêm connection id mới vào key của user đó
                    usersOnline[userId].Add(Context.ConnectionId);
                }
                else
                {
                    // nếu "chưa" thì tạo ra 1 key mới để quản lý connection id cho user đó
                    usersOnline.Add(userId, new List<string> { Context.ConnectionId });
                }

                // thông báo đến toàn bộ client biết user x đã connection
                await Clients.All.SendAsync("onConnected", new Response<object>(new { UserId = userId, isOnline = true }));

                // thông báo cho chính user vừa connect biết có bao nhiu user đang online
                await Clients.Client(Context.ConnectionId).SendAsync("OnGetListUserOnline", new Response<object>(new { UserOnline = usersOnline.Keys, IsOnline = true }));

                // Logs thông tin trên server
                Console.WriteLine($"{userId} connected with connection id = {Context.ConnectionId}");

                Console.WriteLine($"There are {usersOnline.Count} users online");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // lấy  ra userId được gửi lên
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            // kiểm tra userId có null không?
            if (!string.IsNullOrEmpty(userId))
            {
                // kiểm tra user có tồn tại không và connection id của user đang kết nối có tồn tại không?
                if (usersOnline.ContainsKey(userId) && usersOnline[userId].Contains(Context.ConnectionId))
                {
                    // kiểm tra danh sách connection id của user có nhiều hơn 1 connection không?
                    if (GetConnectionIds(userId).Count > 1)
                    {
                        // nếu > 1 => xóa connection id disconnect khỏi danh sách quản lý connection id của user
                        usersOnline[userId].Remove(Context.ConnectionId);
                    }
                    else
                    {
                        // nếu = 1 => remove user key khỏi danh sách quản lý user online
                        // thông báo cho toàn bộ client đang online biết user này đã chính thức offline
                        usersOnline.Remove(userId);

                        await Clients.All.SendAsync("onDisconnected", new Response<object>(new { UserId = userId, isOnline = false }));

                        Console.WriteLine($"There are {usersOnline.Count} users online");
                    }

                    Console.WriteLine($"{userId} connected with connection id = {Context.ConnectionId}");
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
                var response = new Response<object>(new { ConversationId = request.conversationId, SenderId = userId, SenderName = request.senderName, ReceiverId = request.receiverId, Content = request.content, Timming = request.timming });

                foreach (var connectionId in GetConnectionIds(userId))
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", response);
                }

                if (!userId.Equals(request.receiverId))
                {
                    int cnt = 0;
                    foreach (var connectionId in GetConnectionIds(request.receiverId))
                    {
                        ++cnt;
                        await Clients.Client(connectionId).SendAsync("ReceiveMessage", response);

                        // gửi thông báo cho user khi có tin nhắn mới
                        if (cnt == 1)
                            await Clients.Client(connectionId).SendAsync("ReceiveNotificationMessage", new Response<object>(new { Content = $"Bạn có 1 tin nhắn mới từ {request.senderName} vào lúc {request.timming}" }));
                    }
                }
            }
        }

        public async Task ReadMessage(ReadMessageRequest request)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                var response = new Response<object>(new { SenderId = userId, ReceiverId = request.receiverId, Seen = request.seen });

                foreach (var connectionId in GetConnectionIds(userId))
                {
                    await Clients.Client(connectionId).SendAsync("OnReadMessage", response);
                }

                if (!userId.Equals(request.receiverId))
                {
                    foreach (var connectionId in GetConnectionIds(request.receiverId))
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
