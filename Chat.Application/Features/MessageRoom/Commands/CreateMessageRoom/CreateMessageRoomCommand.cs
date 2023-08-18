using AutoMapper;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.MessageRoom.Commands.CreateMessageRoom
{
    public class CreateMessageRoomCommand : IRequest<Response<Domain.Entities.MessageRoom>>
    {
        public string SenderId { get; set; }
        public string Content { get; set; }
        public string RoomId { get; set; }
    }

    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageRoomCommand, Response<Domain.Entities.MessageRoom>>
    {
        private readonly IMessageRoomRepositoryAsync _messageRoomRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateMessageCommandHandler(IMessageRoomRepositoryAsync messageRoomRepositoryAsync, IMapper mapper)
        {
            _messageRoomRepositoryAsync = messageRoomRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<Domain.Entities.MessageRoom>> Handle(CreateMessageRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newMessageRoom = _mapper.Map<Domain.Entities.MessageRoom>(request);

                var result = await _messageRoomRepositoryAsync.InsertOneAsync(newMessageRoom);

                return new Response<Domain.Entities.MessageRoom>(result, "Thành công");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
