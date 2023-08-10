using Chat.Application.Features.User.Commands.CreateUser;
using Chat.Application.Features.User.Queries.GetByNickname;
using Lab.SignalR_Chat.BE.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpGet("SearchUser")]
        public async Task<IActionResult> Get([FromQuery] string nickname)
        {
            return Ok(await Mediator.Send(new GetByNicknameQuery { Nickname = nickname }));
        }

        [HttpPost("{nickname}")]
        public async Task<IActionResult> Post(string nickname)
        {
            return Ok(await Mediator.Send(new CreateUserCommand { Nickname = nickname }));
        }
    }
}
