using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Room.Queries.FindAnyAndGetLatestMessage
{
    public class FindAnyAndGetLatestMessageQuery : IRequest<PagedResponse<IList<FindAnyAndGetLatestMessageViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
    }

    public class FindAnyAndGetLatestMessageQueryHandler : IRequestHandler<FindAnyAndGetLatestMessageQuery, PagedResponse<IList<FindAnyAndGetLatestMessageViewModel>>>
    {
        private readonly IRoomRepositoryAsync _roomRepositoryAsync;

        public FindAnyAndGetLatestMessageQueryHandler(IRoomRepositoryAsync roomRepositoryAsync)
        {
            _roomRepositoryAsync = roomRepositoryAsync;
        }

        public async Task<PagedResponse<IList<FindAnyAndGetLatestMessageViewModel>>> Handle(FindAnyAndGetLatestMessageQuery request, CancellationToken cancellationToken)
        {
            var results = await _roomRepositoryAsync.FindAnyAndGetLatestMessage(request.PageNumber, request.PageSize, request.Keyword);
            return new PagedResponse<IList<FindAnyAndGetLatestMessageViewModel>>(results, request.PageNumber, request.PageSize);
        }
    }
}
