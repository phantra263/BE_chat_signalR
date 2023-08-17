using AutoMapper;
using Chat.API.SignalR.PresenceTracker;
using Chat.Application.Features.Message.Commands.CreateMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.SignalR.Hubs
{
    public class RoomHub : MessageHub
    {
        public RoomHub(IPresenceTracker presenceTracker, IMediator mediator, IMapper mapper) : base(presenceTracker, mediator, mapper)
        {
        }

        public async Task SendMessageRoom()
        {
            await Clients.All.SendAsync("OnReceiveMessageRoom", "");
        }
    }
}
