using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Queries.GetMessages
{
    public class GetMessageQuery : IRequest<PagedResponse<IReadOnlyList<Domain.Entities.Message>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }

    public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, PagedResponse<IReadOnlyList<Domain.Entities.Message>>>
    {
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;

        public GetMessageQueryHandler(IMessageRepositoryAsync messageRepositoryAsync)
        {
            _messageRepositoryAsync = messageRepositoryAsync;
        }

        public async Task<PagedResponse<IReadOnlyList<Domain.Entities.Message>>> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var messages = await _messageRepositoryAsync.GetMessageChatAsync(request.PageNumber, request.PageSize, request.Keyword, request.SenderId, request.ReceiverId);

                return new PagedResponse<IReadOnlyList<Domain.Entities.Message>>(messages);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
