using AutoMapper;
using Chat.Application.Features.Message.Queries.GetByConversationId;
using Lab.SignalR_Chat.BE.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseApiController
    {
        private readonly IMapper _mapper;

        public MessagesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("GetByConversation")]
        public async Task<IActionResult> Get([FromQuery] GetByConversationIdParameter parameter)
        {
            var query = _mapper.Map<GetByConversationIdQuery>(parameter);
            return Ok(await Mediator.Send(query));
        }
    }
}
