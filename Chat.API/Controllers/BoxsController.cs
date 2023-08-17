using AutoMapper;
using Chat.Application.Features.Box.Queries.GetBoxChatByUserId;
using Chat.Application.Features.Box.Queries.GetBoxSelected;
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
    public class BoxsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Box> _box;

        public BoxsController(IMongoDBSettings settings, IMapper mapper)
        {
            _mapper = mapper;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _box = database.GetCollection<Box>(Collections.BoxCollection);
        }

        //[HttpPost]
        //public async Task<IActionResult> Post(CreateBoxParameter parameter)
        //    => Ok(await Mediator.Send(new CreateBoxCommand { User1Id = parameter.User1Id, User2Id = parameter.User2Id }));

        //[HttpGet("GetBoxMessage")]
        //public async Task<IActionResult> Get([FromQuery] GetBoxMessageParameter parameter)
        //    => Ok(await Mediator.Send(_mapper.Map<GetBoxMessageQuery>(parameter)));

        [HttpGet("GetBoxSelected")]
        public async Task<IActionResult> Get([FromQuery] GetBoxSelectedParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<GetBoxSelectedQuery>(parameter)));

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> Get([FromQuery] GetBoxChatByUserIdParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<GetBoxChatByUserIdQuery>(parameter)));

        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> Delete()
        {
            _box.DeleteMany(x => x.Deleted != true);
            return Ok();
        }
    }
}
