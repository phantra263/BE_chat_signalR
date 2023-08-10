using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public string User1Id { get; set; }
        public string User2Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;
        private readonly IMessageRepositoryAsync _messageRepository;

        public DeleteUserCommandHandler(IUserRepositoryAsync userRepositoryAsync, IMessageRepositoryAsync messageRepository, IBoxRepositoryAsync boxRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
            _messageRepository = messageRepository;
            _boxRepositoryAsync = boxRepositoryAsync;
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // del box chat
                await _boxRepositoryAsync.FindAndDeleteByUserAsync(request.User1Id, request.User2Id);



                return new Response<string>("");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
