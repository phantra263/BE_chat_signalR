using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Box.Queries.GetBoxChatWith
{
    public class GetBoxChatWithQuery : IRequest<PagedResponse<IReadOnlyList<Domain.Entities.Box>>>
    {
        public string UserChatWithId { get; set; }
    }

    public class GetBoxChatWithQueryHandler : IRequestHandler<GetBoxChatWithQuery, PagedResponse<IReadOnlyList<Domain.Entities.Box>>>
    {
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;

        public GetBoxChatWithQueryHandler(IBoxRepositoryAsync boxRepositoryAsync)
        {
            _boxRepositoryAsync = boxRepositoryAsync;
        }

        public async Task<PagedResponse<IReadOnlyList<Domain.Entities.Box>>> Handle(GetBoxChatWithQuery request, CancellationToken cancellationToken) 
            => new PagedResponse<IReadOnlyList<Domain.Entities.Box>>(await _boxRepositoryAsync.GetBoxsUserChatWith(request.UserChatWithId));
    }
}
