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
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;

        public CreateMessageCommandHandler(IMessageRepositoryAsync messageRepositoryAsync, IBoxRepositoryAsync boxRepositoryAsync)
        {
            _messageRepositoryAsync = messageRepositoryAsync;
            _boxRepositoryAsync = boxRepositoryAsync;
        }

        public async Task<Response<Domain.Entities.Message>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                /*
                // get thông tin box chat, nếu chưa có thì tạo mới
                var boxChat = await _boxRepositoryAsync.GetCheckExist(request.SenderId, request.ReceiverId);

                // check ReceiverId đã tạo cuộc trò chuyện vs SenderId chưa
                // nếu chưa thì tạo mới cuộc trò chuyện cho ReceiverId
                var boxAccessBefore = await _boxRepositoryAsync.GetCheckUsr2AccessUsr1(request.SenderId, request.ReceiverId);

                if (boxAccessBefore == null)
                {
                    await _boxRepositoryAsync.CreateAsync(new Domain.Entities.Box
                    {
                        User1Id = request.ReceiverId,
                        User2Id = request.SenderId,
                        ConversationId = request.ConversationId
                    });
                }
                */

                // add message vào db
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
