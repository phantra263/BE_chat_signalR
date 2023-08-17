using AutoMapper;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Box.Commands.CreateBoxLatestMessage
{
    public class CreateBoxLatestMessageCommand : IRequest<Response<CreateBoxLatestMessageViewModel>>
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }

    public class CreateBoxLatestMessageCommandHandler : IRequestHandler<CreateBoxLatestMessageCommand, Response<CreateBoxLatestMessageViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepositoryAsync _messageRepositoryAsync;
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public CreateBoxLatestMessageCommandHandler(IMapper mapper, IMessageRepositoryAsync messageRepositoryAsync, IBoxRepositoryAsync boxRepositoryAsync, IUserRepositoryAsync userRepositoryAsync)
        {
            _mapper = mapper;
            _messageRepositoryAsync = messageRepositoryAsync;
            _boxRepositoryAsync = boxRepositoryAsync;
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<CreateBoxLatestMessageViewModel>> Handle(CreateBoxLatestMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sender = await _userRepositoryAsync.GetByIdAsync(request.SenderId);
                var receiver = await _userRepositoryAsync.GetByIdAsync(request.ReceiverId);

                if (receiver == null || sender == null)
                    return new Response<CreateBoxLatestMessageViewModel>("Người dùng không tồn tại");

                var viewModel = new CreateBoxLatestMessageViewModel();

                // get thông tin box chat, nếu chưa có thì tạo mới
                var boxChat = await _boxRepositoryAsync.GetCheckExist(request.SenderId, request.ReceiverId);

                if (boxChat != null)
                    return new Response<CreateBoxLatestMessageViewModel>("Đã tạo hộp thoại trước đó");

                // check user 2 đã tạo hội thoại trước đó chưa
                // nếu chưa thì tạo mới và lấy ra tin nhắn cuối cùng của 2 user
                var boxAccessBefore = await _boxRepositoryAsync.GetCheckUsr2AccessUsr1(request.SenderId, request.ReceiverId);

                var newBoxChat = await _boxRepositoryAsync.CreateAsync(new Domain.Entities.Box
                {
                    User1Id = request.SenderId,
                    User2Id = request.ReceiverId,
                    ConversationId = boxAccessBefore != null ? boxAccessBefore.ConversationId : $"{request.SenderId}{request.ReceiverId}"
                });

                viewModel = _mapper.Map<CreateBoxLatestMessageViewModel>(newBoxChat);

                // lấy ra tin nhắn cuối cùng của 2 user
                var message = await _messageRepositoryAsync.GetLatestMessageChatAsync(request.SenderId, request.ReceiverId);

                viewModel.UserId = receiver.Id;
                viewModel.Nickname = receiver.Nickname;
                viewModel.AvatarBgColor = receiver.AvatarBgColor;
                viewModel.Status = receiver.Status;
                viewModel.IsOnline = receiver.IsOnline;

                viewModel.SenderId = message?.SenderId;
                viewModel.Content = message?.Content;
                viewModel.IsSeen = message?.IsSeen;
                viewModel.Created = message?.Created;

                return new Response<CreateBoxLatestMessageViewModel>(viewModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
