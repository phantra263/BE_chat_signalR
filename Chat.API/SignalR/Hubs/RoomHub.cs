using AutoMapper;
using Chat.API.SignalR.PresenceTracker;
using Chat.Application.DTOs.SignalDtos;
using Chat.Application.Enums;
using Chat.Application.Features.MessageRoom.Commands.CreateMessageRoom;
using Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom;
using Chat.Application.Features.Room.Queries.FindOneAndGetLatestMessage;
using Chat.Application.Features.User.Commands.UpdateUserChatRoom;
using Chat.Application.Features.User.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.API.SignalR.Hubs
{
    public class RoomHub : MessageHub
    {
        private readonly IPresenceTracker _presenceTracker;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoomHub(IPresenceTracker presenceTracker, IMediator mediator, IMapper mapper) : base(presenceTracker, mediator, mapper)
        {
            _presenceTracker = presenceTracker;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task PushRoomToAny(string roomId)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId)) return;

            var result = await _mediator.Send(new FindOneAndGetLatestMessageQuery { RoomId = roomId });

            if (!result.Succeeded) return;

            await Clients.All.SendAsync("OnPushRoomToAny", result.Data);
        }

        public async Task PushAnyNotiJoinRoom(string roomId)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId)) return;

            var avatarIdRandom = $"{userId}{new Random().Next(1, 5000 + 1)}";
            string randomName = GenerateRandomName(Enums.Animals, Enums.Colors, Enums.States);

            var commandResult = await _mediator.Send(new UpdateUserChatRoomCommand { Id = userId,  AvatarId = avatarIdRandom, AnonymousName = randomName });
            if (!commandResult.Succeeded) return;

            await Clients.All.SendAsync("OnPushAnyNotiJoinRoom", new PushAnyNotiJoinRoomResponse { roomId = roomId, userId = userId, avatarId = avatarIdRandom, anonymousName = randomName });
        }

        public async Task SendMessageToRoom(CreateMessageRoomParameter parameter)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

            if (string.IsNullOrEmpty(userId)) return;

            var userInfo = await _mediator.Send(new GetByIdQuery { Id = userId });
            if (!userInfo.Succeeded) return;

            var command = _mapper.Map<CreateMessageRoomCommand>(parameter);
            command.SenderId = userId;

            var commandResult = await _mediator.Send(command);
            if (!commandResult.Succeeded) return;

            await Clients.All.SendAsync("OnReceiveMessageRoom", new HistoryMessageRoomViewModel
            {
                Content = commandResult.Data.Content,
                Created = commandResult.Data.Created,
                SenderId = userInfo.Data.Id,
                AvatarId = userInfo.Data.AvatarId,
                AnonymousName = userInfo.Data.AnonymousName
            });
        }

        static string GenerateRandomName(List<string> animals, List<string> colors, List<string> states)
        {
            Random random = new Random();
            string randomAnimal = animals[random.Next(animals.Count)];
            string randomState = states[random.Next(states.Count)];
            string randomColor = colors[random.Next(colors.Count)];
            string randomName = $"{char.ToUpper(randomAnimal[0]) + randomAnimal.Substring(1)} {randomColor.ToLower()} {randomState.ToLower()}";
            return randomName;
        }
    }
}
