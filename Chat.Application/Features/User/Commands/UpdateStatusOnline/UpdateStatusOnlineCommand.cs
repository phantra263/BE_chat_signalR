using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.User.Commands.UpdateStatusOnline
{
    public class UpdateStatusOnlineCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public bool IsOnline { get; set; }
    }

    public class UpdateStatusOnlineCommandHandler : IRequestHandler<UpdateStatusOnlineCommand, Response<string>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public UpdateStatusOnlineCommandHandler(IUserRepositoryAsync userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<string>> Handle(UpdateStatusOnlineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepositoryAsync.GetByIdAsync(request.Id);

                if (user == null)
                    return new Response<string>("User không tồn tại");

                user.IsOnline = request.IsOnline;

                await _userRepositoryAsync.UpdateAsync(request.Id, user);

                return new Response<string>("Update trang thái thành công");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
