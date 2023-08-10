using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Response<Domain.Entities.User>>
    {
        public string Nickname { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<Domain.Entities.User>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public CreateUserCommandHandler(IUserRepositoryAsync userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<Domain.Entities.User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userExist = await _userRepositoryAsync.GetByNicknameAsync(request.Nickname);
                if (userExist != null)
                    return new Response<Domain.Entities.User>($"Nickname đã tồn tại");

                var newUser = await _userRepositoryAsync.CreateAsync(new Domain.Entities.User
                {
                    Created = DateTime.Now,
                    Nickname = request.Nickname,
                    Status = true
                });

                return new Response<Domain.Entities.User>(newUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
