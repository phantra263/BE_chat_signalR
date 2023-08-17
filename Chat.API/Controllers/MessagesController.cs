using AutoMapper;
using Chat.Application.Features.Message.Commands.UpdateMessage;
using Chat.Application.Features.Message.Queries.GetByConversationId;
using Chat.Domain.Constants;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence.MongoDBSetting;
using Lab.SignalR_Chat.BE.Controllers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Message> _message;

        public MessagesController(IMongoDBSettings settings, IMapper mapper)
        {
            _mapper = mapper;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _message = database.GetCollection<Message>(Collections.BoxCollection);
        }

        [HttpGet("GetByConversation")]
        public async Task<IActionResult> Get([FromQuery] GetByConversationIdParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<GetByConversationIdQuery>(parameter)));

        [HttpPut("UpdateSeenMessage")]
        public async Task<IActionResult> Put(UpdateMessageParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<UpdateMessageCommand>(parameter)));

        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> Delete()
        {
            await _message.DeleteManyAsync("{ Deleted : true }");
            return Ok();
        }
    }
}
