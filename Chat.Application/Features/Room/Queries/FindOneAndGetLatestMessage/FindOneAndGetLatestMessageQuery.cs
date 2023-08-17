using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Room.Queries.FindOneAndGetLatestMessage
{
    public class FindOneAndGetLatestMessageQuery : IRequest<Response<FindOneAndGetLatestMessageViewModel>>
    {
        public string RoomId { get; set; }
    }

    public class FindOneAndGetLatestMessageQueryHandler : IRequestHandler<FindOneAndGetLatestMessageQuery, Response<FindOneAndGetLatestMessageViewModel>>
    {
        private readonly IRoomRepositoryAsync _roomRepositoryAsync;

        public FindOneAndGetLatestMessageQueryHandler(IRoomRepositoryAsync roomRepositoryAsync)
        {
            _roomRepositoryAsync = roomRepositoryAsync;
        }

        public async Task<Response<FindOneAndGetLatestMessageViewModel>> Handle(FindOneAndGetLatestMessageQuery request, CancellationToken cancellationToken)
            => new Response<FindOneAndGetLatestMessageViewModel>(await _roomRepositoryAsync.FindOneAndGetLatestMessage(request.RoomId));
    }
}
