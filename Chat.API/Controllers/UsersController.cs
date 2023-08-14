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
        public async Task<IActionResult> Get([FromQuery] GetByNicknameParameter parameter)
        {
            return Ok(await Mediator.Send(new GetByNicknameQuery { Nickname = parameter.Nickname, YourNickname = parameter.YourNickname }));
        }

        [HttpPost("CreateNickname")]
        public async Task<IActionResult> Post(CreateUserParameter parameter)
        {
            return Ok(await Mediator.Send(new CreateUserCommand { Nickname = parameter.Nickname, AvatarBgColor = parameter.AvatarBgColor }));
        }
    }
}
