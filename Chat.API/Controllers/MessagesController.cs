using AutoMapper;
using Chat.Application.Features.Message.Queries.GetMessages;
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

        [HttpGet("GetMessageBox")]
        public async Task<IActionResult> Get([FromQuery] GetMessageParameter parameter)
        {
            var query = _mapper.Map<GetMessageQuery>(parameter);

            return Ok(await Mediator.Send(query));
        }
    }
}
