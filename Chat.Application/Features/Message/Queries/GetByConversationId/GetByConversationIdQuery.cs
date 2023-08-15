using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Message.Queries.GetByConversationId
{
    public class GetByConversationIdQuery : IRequest<Response<GetByConversationIdViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
    }

    public class GetByConversationIdQueryHandler : IRequestHandler<GetByConversationIdQuery, Response<GetByConversationIdViewModel>>
    {
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;

        public GetByConversationIdQueryHandler(IMessageRepositoryAsync messageRepositoryAsync, IUserRepositoryAsync userRepositoryAsync, IBoxRepositoryAsync boxRepositoryAsync)
        {
            _messageRepositoryAsync = messageRepositoryAsync;
            _userRepositoryAsync = userRepositoryAsync;
            _boxRepositoryAsync = boxRepositoryAsync;
        }

        public async Task<Response<GetByConversationIdViewModel>> Handle(GetByConversationIdQuery request, CancellationToken cancellationToken)
        {
            var boxs = await _boxRepositoryAsync.GetByConversationId(request.ConversationId);

            if (boxs.Count == 0)
                return new Response<GetByConversationIdViewModel>("Cuộc trò chuyện không tồn tại");

            var user = await _userRepositoryAsync.GetByIdAsync(boxs.Where(x => x.User1Id == request.UserId && x.User2Id != request.UserId).FirstOrDefault().User2Id);

            var historyMessage = await _messageRepositoryAsync.GetMessageByConversation(request.PageNumber, request.PageSize, request.Keyword, request.ConversationId);

            return new Response<GetByConversationIdViewModel>(new GetByConversationIdViewModel
            {
                Id = user.Id,
                Nickname = user.Nickname,
                AvatarBgColor = user.AvatarBgColor,
                Status = user.Status,
                IsOnline = user.IsOnline,
                Messages = historyMessage
            });
        }
    }
}
