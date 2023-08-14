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

        [HttpGet("{conversationId}")]
        public async Task<IActionResult> Get(string conversationId)
            => Ok(await Mediator.Send(new GetByConversationIdQuery { ConversationId = conversationId }));
    }
}
