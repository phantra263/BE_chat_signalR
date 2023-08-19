using AutoMapper;
using Chat.Application.Features.MessageRoom.Commands.CreateMessageRoom;
using Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageRoomsController : BaseApiController
    {
        private readonly IMapper _mapper;

        public MessageRoomsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("GetMessageInRoom")]
        public async Task<IActionResult> Get([FromQuery] GetLatestMessageInRoomParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<GetLatestMessageInRoomQuery>(parameter)));
    }
}
