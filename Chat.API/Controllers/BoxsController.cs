using AutoMapper;
using Chat.Application.Features.Box.Commands.CreateBox;
using Chat.Application.Features.Box.Queries.GetBoxLatestMessage;
using Chat.Application.Features.Box.Queries.GetBoxMessage;
using Lab.SignalR_Chat.BE.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxsController : BaseApiController
    {
        private readonly IMapper _mapper;

        public BoxsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBoxParameter parameter)
        {
            return Ok(await Mediator.Send(new CreateBoxCommand { User1Id = parameter.User1Id, User2Id = parameter.User2Id }));
        }

        [HttpGet("GetBoxLatestMessage")]
        public async Task<IActionResult> Get([FromQuery] GetBoxLatestMessageParameter parameter)
        {
            var query = _mapper.Map<GetBoxLatestMessageQuery>(parameter);

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("GetBoxMessage")]
        public async Task<IActionResult> Get([FromQuery] GetBoxMessageParameter parameter)
        {
            var query = _mapper.Map<GetBoxMessageQuery>(parameter);

            return Ok(await Mediator.Send(query));
        }
    }
}
