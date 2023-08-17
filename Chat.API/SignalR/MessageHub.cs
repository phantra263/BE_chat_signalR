using AutoMapper;
using Chat.API.SignalR.PresenceTracker;
using Chat.Application.Features.Box.Commands.CreateBoxLatestMessage;
using Chat.Application.Features.Box.Queries.GetBoxChatWith;
using Chat.Application.Features.Message.Commands.CreateMessage;
using Chat.Application.Features.Message.Commands.UpdateMessage;
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
        private readonly IMapper _mapper;
        //private readonly IHostedService _backgroundService;

        public MessageHub(IPresenceTracker presenceTracker, IMediator mediator, IMapper mapper)
        {
            _presenceTracker = presenceTracker;
            _mediator = mediator;
            _mapper = mapper;
            //_backgroundService = backgroundService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId)) return;

            var isUserOnline = await _presenceTracker.UserConnected(userId, Context.ConnectionId);

            var updStatus = await _mediator.Send(new UpdateStatusOnlineCommand { Id = userId, IsOnline = true });
            if (!updStatus.Succeeded) return;

            var listBoxChatWith = await _mediator.Send(new GetBoxChatWithQuery { UserChatWithId = userId });
            if (!listBoxChatWith.Succeeded) return;

            foreach (var item in listBoxChatWith.Data)
            {
                var connectionIds = _presenceTracker.GetConnectionIds(item.User1Id);
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

            if (string.IsNullOrEmpty(userId)) return;

            var isUserOffline = await _presenceTracker.UserDisconnected(userId, Context.ConnectionId);

            if (!isUserOffline) return;

            var updStatus = await _mediator.Send(new UpdateStatusOnlineCommand { Id = userId, IsOnline = false });
            if (!updStatus.Succeeded)
                return;

            var listBoxChatWith = await _mediator.Send(new GetBoxChatWithQuery { UserChatWithId = userId });
            if (!listBoxChatWith.Succeeded) return;

            foreach (var item in listBoxChatWith.Data)
            {
                var connectionIds = _presenceTracker.GetConnectionIds(item.User1Id);
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

        public async Task SendMessage(CreateMessageParameter parameter)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId)) return;

            // add message to db
            var command = _mapper.Map<CreateMessageCommand>(parameter);
            command.SenderId = userId;

            var result = await _mediator.Send(command);

            if (!result.Succeeded) return;

            // khoi tao view model return
            var viewModel = _mapper.Map<CreateMessageViewModel>(result.Data);
            viewModel.SenderId = userId;
            viewModel.SenderName = parameter.SenderName;
            viewModel.ReceiverName = parameter.ReceiverName;

            // tao hop thoai chat neu chua ton tai
            var queryResult = await _mediator.Send(new CreateBoxLatestMessageCommand { SenderId = parameter.ReceiverId, ReceiverId = userId });

            // send signal den cac connection cua chinh sender
            var senderConnectionIds = _presenceTracker.GetConnectionIds(userId);
            foreach (var connectionId in senderConnectionIds)
            {
                await Clients.Client(connectionId).SendAsync("OnReceiveMessage", viewModel);
            }

            // send signal den cac connection cua receiver
            var receiverConnectionIds = _presenceTracker.GetConnectionIds(parameter.ReceiverId);
            if (!userId.Equals(parameter.ReceiverId))   // nếu nó ko tự gửi cho chính nó
            {
                int cnt = 0;
                foreach (var connectionId in receiverConnectionIds)
                {
                    ++cnt;
                    await Clients.Client(connectionId).SendAsync("OnReceiveMessage", viewModel);

                    if(queryResult.Succeeded)
                        await Clients.Client(connectionId).SendAsync("OnReceiveNewMessageBox", queryResult.Data);

                    // gửi thông báo cho user khi có tin nhắn mới
                    if (cnt == 1)
                        await Clients.Client(connectionId).SendAsync("OnReceiveNotificationMessage", new Response<object>(new { Content = $"Bạn có 1 tin nhắn mới từ {parameter.SenderName}: \n{parameter.Content}" }));
                }
            }
        }

        public async Task ReadMessage(UpdateMessageParameter parameter)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId)) return;

            var command = _mapper.Map<UpdateMessageCommand>(parameter);

            var result = await _mediator.Send(command);
            if (!result.Succeeded) return;

            var senderConnectionIds = _presenceTracker.GetConnectionIds(userId);
            foreach (var connectionId in senderConnectionIds)
            {
                await Clients.Client(connectionId).SendAsync("OnReadMessage", result.Data);
            }

            var receiverConnectionIds = _presenceTracker.GetConnectionIds(userId == result.Data.ReceiverId ? result.Data.SenderId : result.Data.ReceiverId);
            foreach (var connectionId in receiverConnectionIds)
            {
                await Clients.Client(connectionId).SendAsync("OnReadMessage", result.Data);
            }
        }
    }
}
