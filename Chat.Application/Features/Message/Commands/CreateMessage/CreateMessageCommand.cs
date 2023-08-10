using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest<Response<Domain.Entities.Message>>
    {
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
    }

    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Response<Domain.Entities.Message>>
    {
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;

        public CreateMessageCommandHandler(IMessageRepositoryAsync messageRepositoryAsync)
        {
            _messageRepositoryAsync = messageRepositoryAsync;
        }

        public async Task<Response<Domain.Entities.Message>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var message = await _messageRepositoryAsync.CreateAsync(new Domain.Entities.Message
                {
                    ConversationId = request.ConversationId,
                    SenderId = request.SenderId,
                    ReceiverId = request.ReceiverId,
                    Content = request.Content
                });

                return new Response<Domain.Entities.Message>(message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
