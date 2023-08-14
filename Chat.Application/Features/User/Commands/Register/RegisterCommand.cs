using AutoMapper;
using Chat.Application.Helpers;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.User.Commands.Register
{
    public class RegisterCommand : IRequest<Response<RegisterViewModel>>
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string AvatarBgColor { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<RegisterViewModel>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUserRepositoryAsync userRepositoryAsync, IMapper mapper)
        {
            _userRepositoryAsync = userRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<RegisterViewModel>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userExist = await _userRepositoryAsync.GetByNicknameAsync(request.Nickname);
                if (userExist != null)
                    return new Response<RegisterViewModel>($"Nickname đã tồn tại");

                var newUser = await _userRepositoryAsync.CreateAsync(new Domain.Entities.User
                {
                    Nickname = request.Nickname,
                    Password = Hash.MD5Encode(request.Password),
                    AvatarBgColor = request.AvatarBgColor,
                    Status = true,
                    IsOnline = true
                });

                var viewModel = _mapper.Map<RegisterViewModel>(newUser);

                return new Response<RegisterViewModel>(viewModel, "Đăng ký thành công");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
