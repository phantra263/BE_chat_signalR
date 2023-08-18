using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.User.Commands.UpdateUserChatRoom
{
    public class UpdateUserChatRoomCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
        public string AvatarId { get; set; }
        public string AnonymousName { get; set; }
    }

    public class UpdateAvatarIdCommandHandler : IRequestHandler<UpdateUserChatRoomCommand, Response<string>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public UpdateAvatarIdCommandHandler(IUserRepositoryAsync userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<string>> Handle(UpdateUserChatRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepositoryAsync.GetByIdAsync(request.Id);
                if (user == null)
                    return new Response<string>("User không tồn tại");

                user.AvatarId = string.IsNullOrEmpty(request.AvatarId) ? user.AvatarId : request.AvatarId;
                user.AnonymousName = string.IsNullOrEmpty(request.AnonymousName) ? user.AnonymousName : request.AnonymousName;

                await _userRepositoryAsync.UpdateAsync(request.Id, user);

                return new Response<string>(user.Id, "Cập nhật thành công");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
