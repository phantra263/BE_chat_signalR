using AutoMapper;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Box.Queries.GetBoxMessage
{
    public class GetBoxMessageQuery : IRequest<PagedResponse<GetBoxMessageViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }

    public class GetMessageQueryHandler : IRequestHandler<GetBoxMessageQuery, PagedResponse<GetBoxMessageViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;

        public GetMessageQueryHandler(IMessageRepositoryAsync messageRepositoryAsync, IBoxRepositoryAsync boxRepositoryAsync, IMapper mapper)
        {
            _messageRepositoryAsync = messageRepositoryAsync;
            _boxRepositoryAsync = boxRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetBoxMessageViewModel>> Handle(GetBoxMessageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var viewModel = new GetBoxMessageViewModel();

                // get thông tin box chat, nếu chưa có thì tạo mới
                var boxChat = await _boxRepositoryAsync.GetCheckExist(request.SenderId, request.ReceiverId);

                // check user 2 đã tạo hội thoại trước đó chưa
                var boxAccessBefore = await _boxRepositoryAsync.GetCheckUsr2AccessUsr1(request.SenderId, request.ReceiverId);

                if (boxChat == null)
                {
                    var newBoxChat = await _boxRepositoryAsync.CreateAsync(new Domain.Entities.Box
                    {
                        User1Id = request.SenderId,
                        User2Id = request.ReceiverId,
                        ConversationId = boxAccessBefore != null ? boxAccessBefore.ConversationId : $"{request.SenderId}{request.ReceiverId}"
                    });

                    viewModel = _mapper.Map<GetBoxMessageViewModel>(newBoxChat);
                }
                else
                {
                    viewModel = _mapper.Map<GetBoxMessageViewModel>(boxChat);
                }

                var messages = await _messageRepositoryAsync.GetMessageChatAsync(request.PageNumber, request.PageSize, request.Keyword, request.SenderId, request.ReceiverId);

                viewModel.Messages = messages;

                return new PagedResponse<GetBoxMessageViewModel>(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
