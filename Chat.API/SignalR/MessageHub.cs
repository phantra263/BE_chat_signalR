using Chat.API.Models.Requests;
using Chat.API.SignalR.PresenceTracker;
using Chat.Application.Features.Box.Queries.GetBoxChatWith;
using Chat.Application.Features.User.Commands.UpdateStatusOnline;
using Chat.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Chat.API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IPresenceTracker _presenceTracker;
        private readonly IMediator _mediator;
        //private readonly IHostedService _backgroundService;

        public MessageHub(IPresenceTracker presenceTracker, IMediator mediator)
        {
            _presenceTracker = presenceTracker;
            _mediator = mediator;
            //_backgroundService = backgroundService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId))
                return;

            var isUserOnline = await _presenceTracker.UserConnected(userId, Context.ConnectionId);

            var updStatus = await _mediator.Send(new UpdateStatusOnlineCommand { Id = userId, IsOnline = true });
            if (!updStatus.Succeeded)
                return;

            var listBoxChatWith = await _mediator.Send(new GetBoxChatWithQuery { UserChatWithId = userId });
            if (!listBoxChatWith.Succeeded)
                return;

            foreach (var item in listBoxChatWith.Data)
            {
                var connectionIds = await _presenceTracker.GetConnectionIds(item.User1Id);
                foreach (var connectionId in connectionIds)
                {
                    await Clients.Client(connectionId).SendAsync("OnConnected", new Response<object>(new { UserId = userId, IsOnline = isUserOnline }));
                }
            }

            //if (_backgroundService is Worker worker)
            //    worker.CancelTask(userId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId))
                return;

            var isUserOffline = await _presenceTracker.UserDisconnected(userId, Context.ConnectionId);

            if (!isUserOffline)
                return;

            var updStatus = await _mediator.Send(new UpdateStatusOnlineCommand { Id = userId, IsOnline = false });
            if (!updStatus.Succeeded)
                return;

            var listBoxChatWith = await _mediator.Send(new GetBoxChatWithQuery { UserChatWithId = userId });
            if (!listBoxChatWith.Succeeded)
                return;

            foreach (var item in listBoxChatWith.Data)
            {
                var connectionIds = await _presenceTracker.GetConnectionIds(item.User1Id);
                foreach (var connectionId in connectionIds)
                {
                    await Clients.Client(connectionId).SendAsync("OnDisconnected", new Response<object>(new { UserId = userId, IsOnline = false }));
                }
            }

            //if (_backgroundService is Worker worker)
            //    worker.TriggerTask(userId);

            await base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing) => base.Dispose(disposing);

        public async Task SendMessage(MessageRequest request)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId))
                return;

            var response = new Response<object>();

            var senderConnectionIds = await _presenceTracker.GetConnectionIds(userId);
            foreach (var connectionId in senderConnectionIds)
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", response);
            }

            var receiverConnectionIds = await _presenceTracker.GetConnectionIds(request.receiverId);
            if (!userId.Equals(request.receiverId))
            {
                int cnt = 0;
                foreach (var connectionId in receiverConnectionIds)
                {
                    ++cnt;
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", response);

                    // gửi thông báo cho user khi có tin nhắn mới
                    if (cnt == 1)
                        await Clients.Client(connectionId).SendAsync("ReceiveNotificationMessage", new Response<object>(new { Content = $"Bạn có 1 tin nhắn mới từ {request.senderName}: \n{request.content}" }));
                }
            }
        }

        public async Task ReadMessage(ReadMessageRequest request)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                var response = new Response<object>(new { SenderId = userId, ReceiverId = request.receiverId, Seen = request.seen });

                var senderConnectionIds = await _presenceTracker.GetConnectionIds(userId);
                foreach (var connectionId in senderConnectionIds)
                {
                    await Clients.Client(connectionId).SendAsync("OnReadMessage", response);
                }

                var receiverConnectionIds = await _presenceTracker.GetConnectionIds(request.receiverId);
                if (!userId.Equals(request.receiverId))
                {
                    foreach (var connectionId in receiverConnectionIds)
                    {
                        await Clients.Client(connectionId).SendAsync("OnReadMessage", response);
                    }
                }
            }
        }
    }
}
