using AutoMapper;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom
{
    public class GetLatestMessageInRoomQuery : IRequest<Response<GetLatestMessageInRoomViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string RoomId { get; set; }
    }

    public class GetLatestMessageInRoomQueryHandler : IRequestHandler<GetLatestMessageInRoomQuery, Response<GetLatestMessageInRoomViewModel>>
    {
        private readonly IRoomRepositoryAsync _roomRepositoryAsync;
        private readonly IMessageRoomRepositoryAsync _messageRoomRepositoryAsync;
        private readonly IMapper _mapper;

        public GetLatestMessageInRoomQueryHandler(IRoomRepositoryAsync roomRepositoryAsync, IMessageRoomRepositoryAsync messageRoomRepositoryAsync, IMapper mapper)
        {
            _roomRepositoryAsync = roomRepositoryAsync;
            _messageRoomRepositoryAsync = messageRoomRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<GetLatestMessageInRoomViewModel>> Handle(GetLatestMessageInRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var room = await _roomRepositoryAsync.FindOneByIdAsync(request.RoomId);
                if (room == null)
                    return new Response<GetLatestMessageInRoomViewModel>("Room không tồn tại");

                var messages = await _messageRoomRepositoryAsync.GetMessageInRoom(request.PageNumber, request.PageSize, request.Keyword, request.RoomId);

                var viewModel = _mapper.Map<GetLatestMessageInRoomViewModel>(room);
                viewModel.Messages = messages;

                return new Response<GetLatestMessageInRoomViewModel>(viewModel);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
