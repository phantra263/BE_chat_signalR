using AutoMapper;
using Chat.Application.Helpers;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.User.Commands.Authenticate
{
    public class AuthenticateCommand : IRequest<Response<AuthenticateViewModel>>
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticateViewModel>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly IMapper _mapper;

        public AuthenticateCommandHandler(IUserRepositoryAsync userRepositoryAsync, IMapper mapper)
        {
            _userRepositoryAsync = userRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<AuthenticateViewModel>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepositoryAsync.Authenticate(request.Nickname, Hash.MD5Encode(request.Password));
                if (user == null)
                    return new Response<AuthenticateViewModel>($"Thông tin đăng nhập không chính xác");

                var viewModel = _mapper.Map<AuthenticateViewModel>(user);

                return new Response<AuthenticateViewModel>(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
