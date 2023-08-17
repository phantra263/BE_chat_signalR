﻿using AutoMapper;
using Chat.Application.Features.Room.Commands.CreateRoom;
using Chat.Application.Features.Room.Queries.FindOneAndGetLatestMessage;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : BaseApiController
    {
        private readonly IMapper _mapper;

        public RoomsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost("CreateRoom")]
        public async Task<IActionResult> Post(CreateRoomParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<CreateRoomCommand>(parameter)));

        [HttpGet("{roomId}")]
        public async Task<IActionResult> Get(string roomId)
            => Ok(await Mediator.Send(new FindOneAndGetLatestMessageQuery { RoomId = roomId }));
    }
}
