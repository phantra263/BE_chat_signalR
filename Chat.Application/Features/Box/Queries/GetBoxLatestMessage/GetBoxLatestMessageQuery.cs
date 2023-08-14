using AutoMapper;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Box.Queries.GetBoxLatestMessage
{
    public class GetBoxLatestMessageQuery : IRequest<Response<GetBoxLatestMessageViewModel>>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }

    public class GetLatestMessageBoxQueryHandler : IRequestHandler<GetBoxLatestMessageQuery, Response<GetBoxLatestMessageViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;

        public GetLatestMessageBoxQueryHandler(IMapper mapper, IMessageRepositoryAsync messageRepositoryAsync, IBoxRepositoryAsync boxRepositoryAsync)
        {
            _mapper = mapper;
            _messageRepositoryAsync = messageRepositoryAsync;
            _boxRepositoryAsync = boxRepositoryAsync;
        }

        public async Task<Response<GetBoxLatestMessageViewModel>> Handle(GetBoxLatestMessageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var viewModel = new GetBoxLatestMessageViewModel();

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

                    viewModel = _mapper.Map<GetBoxLatestMessageViewModel>(newBoxChat);
                }
                else
                {
                    viewModel = _mapper.Map<GetBoxLatestMessageViewModel>(boxChat);
                }

                var message = await _messageRepositoryAsync.GetLatestMessageChatAsync(request.SenderId, request.ReceiverId);

                viewModel.Message = message;

                return new Response<GetBoxLatestMessageViewModel>(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
