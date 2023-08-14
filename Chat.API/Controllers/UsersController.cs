using AutoMapper;
using Chat.Application.Features.User.Commands.Authenticate;
using Chat.Application.Features.User.Commands.Register;
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
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("SearchUser")]
        public async Task<IActionResult> Get([FromQuery] GetByNicknameParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<GetByNicknameQuery>(parameter)));

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Post(AuthenticateParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<AuthenticateCommand>(parameter)));

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post(RegisterParameter parameter)
            => Ok(await Mediator.Send(_mapper.Map<RegisterCommand>(parameter)));
    }
}
