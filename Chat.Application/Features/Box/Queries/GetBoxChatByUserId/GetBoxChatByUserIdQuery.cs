using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Box.Queries.GetBoxChatByUserId
{
    public class GetBoxChatByUserIdQuery : IRequest<PagedResponse<IReadOnlyList<GetBoxChatByUserIdViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string UserId { get; set; }
    }

    public class GetBoxChatByNicknameQueryHandler : IRequestHandler<GetBoxChatByUserIdQuery, PagedResponse<IReadOnlyList<GetBoxChatByUserIdViewModel>>>
    {
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;

        public GetBoxChatByNicknameQueryHandler(IBoxRepositoryAsync boxRepositoryAsync)
        {
            _boxRepositoryAsync = boxRepositoryAsync;
        }

        public async Task<PagedResponse<IReadOnlyList<GetBoxChatByUserIdViewModel>>> Handle(GetBoxChatByUserIdQuery request, CancellationToken cancellationToken)
            => new PagedResponse<IReadOnlyList<GetBoxChatByUserIdViewModel>>(await _boxRepositoryAsync.GetBoxChatByUserId(request.PageNumber, request.PageSize, request.Keyword, request.UserId), request.PageNumber, request.PageSize);
    }
}
