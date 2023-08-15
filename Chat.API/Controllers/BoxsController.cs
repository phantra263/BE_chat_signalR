using AutoMapper;
using Chat.Application.Features.Box.Queries.GetBoxChatByUserId;
using Chat.Application.Features.Box.Queries.GetBoxLatestMessage;
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

        //[HttpPost]
        //public async Task<IActionResult> Post(CreateBoxParameter parameter)
        //    => Ok(await Mediator.Send(new CreateBoxCommand { User1Id = parameter.User1Id, User2Id = parameter.User2Id }));

        //[HttpGet("GetBoxMessage")]
        //public async Task<IActionResult> Get([FromQuery] GetBoxMessageParameter parameter)
        //    => Ok(await Mediator.Send(_mapper.Map<GetBoxMessageQuery>(parameter)));

        [HttpGet("GetBoxSelected")]
        public async Task<IActionResult> Get([FromQuery] GetBoxLatestMessageParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<GetBoxLatestMessageQuery>(parameter)));

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> Get([FromQuery] GetBoxChatByUserIdParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<GetBoxChatByUserIdQuery>(parameter)));
    }
}
