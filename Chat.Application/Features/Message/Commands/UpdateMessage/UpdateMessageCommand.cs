using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Commands.UpdateMessage
{
    public class UpdateMessageCommand : IRequest<Response<Domain.Entities.Message>>
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public bool IsSeen { get; set; }
    }

    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Response<Domain.Entities.Message>>
    {
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;

        public UpdateMessageCommandHandler(IMessageRepositoryAsync messageRepositoryAsync, IBoxRepositoryAsync boxRepositoryAsync)
        {
            _messageRepositoryAsync = messageRepositoryAsync;
            _boxRepositoryAsync = boxRepositoryAsync;
        }

        public async Task<Response<Domain.Entities.Message>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    await _messageRepositoryAsync.UpdateOneById(request.Id); 
                    
                    var message = await _messageRepositoryAsync.GetByIdAsync(request.Id);

                    return new Response<Domain.Entities.Message>(message, "Cập nhật thành công");
                }
                else
                {
                    await _messageRepositoryAsync.UpdateManyByConversation(request.ConversationId);

                    var message = await _messageRepositoryAsync.GetLatestMessageByConversation(request.ConversationId);

                    return new Response<Domain.Entities.Message>(message, "Cập nhật thành công");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
