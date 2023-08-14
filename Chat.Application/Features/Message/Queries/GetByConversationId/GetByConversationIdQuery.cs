using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Queries.GetMessages
{
    public class GetByConversationIdQuery : IRequest<Response<IReadOnlyList<Domain.Entities.Message>>>
    {
        public string ConversationId { get; set; }
    }

    public class GetByConversationIdQueryHandler : IRequestHandler<GetByConversationIdQuery, Response<IReadOnlyList<Domain.Entities.Message>>>
    {
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;

        public GetByConversationIdQueryHandler(IMessageRepositoryAsync messageRepositoryAsync)
        {
            _messageRepositoryAsync = messageRepositoryAsync;
        }

        public async Task<Response<IReadOnlyList<Domain.Entities.Message>>> Handle(GetByConversationIdQuery request, CancellationToken cancellationToken)
            => new Response<IReadOnlyList<Domain.Entities.Message>>(await _messageRepositoryAsync.GetMessageByConversation(request.ConversationId));
    }
}
