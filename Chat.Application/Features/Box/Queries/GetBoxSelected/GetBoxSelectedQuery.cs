using AutoMapper;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Box.Queries.GetBoxSelected
{
    public class GetBoxSelectedQuery : IRequest<Response<GetBoxSelectedViewModel>>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }

    public class GetLatestMessageBoxQueryHandler : IRequestHandler<GetBoxSelectedQuery, Response<GetBoxSelectedViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public GetLatestMessageBoxQueryHandler(IMapper mapper, IMessageRepositoryAsync messageRepositoryAsync, IBoxRepositoryAsync boxRepositoryAsync, IUserRepositoryAsync userRepositoryAsync)
        {
            _mapper = mapper;
            _messageRepositoryAsync = messageRepositoryAsync;
            _boxRepositoryAsync = boxRepositoryAsync;
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<GetBoxSelectedViewModel>> Handle(GetBoxSelectedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sender = await _userRepositoryAsync.GetByIdAsync(request.SenderId);
                var receiver = await _userRepositoryAsync.GetByIdAsync(request.ReceiverId);

                if (receiver == null || sender == null)
                    return new Response<GetBoxSelectedViewModel>("Người dùng không tồn tại");

                var viewModel = new GetBoxSelectedViewModel();

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

                    viewModel = _mapper.Map<GetBoxSelectedViewModel>(newBoxChat);
                }
                else
                {
                    viewModel = _mapper.Map<GetBoxSelectedViewModel>(boxChat);
                }

                var message = await _messageRepositoryAsync.GetLatestMessageChatAsync(request.SenderId, request.ReceiverId);

                viewModel.UserId = receiver.Id;
                viewModel.Nickname = receiver.Nickname;
                viewModel.AvatarBgColor = receiver.AvatarBgColor;
                viewModel.Status = receiver.Status;
                viewModel.IsOnline = receiver.IsOnline;

                viewModel.Content = message?.Content;
                viewModel.IsSeen = message?.IsSeen;
                viewModel.Created = message?.Created;

                return new Response<GetBoxSelectedViewModel>(viewModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
