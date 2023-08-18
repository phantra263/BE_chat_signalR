using AutoMapper;
using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Room.Commands.CreateRoom
{
    public class CreateRoomCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string UserId { get; set; }
    }

    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Response<string>>
    {
        private readonly IRoomRepositoryAsync _roomRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IRoomRepositoryAsync roomRepositoryAsync, IMapper mapper)
        {
            _roomRepositoryAsync = roomRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var roomExist = await _roomRepositoryAsync.FindOneByNameAsync(request.Name);
                if (roomExist != null)
                    return new Response<string>($"Tên phòng {request.Name} đã tồn tại");

                var result = await _roomRepositoryAsync.InsertOneAsync(_mapper.Map<Domain.Entities.Room>(request));

                return new Response<string>(result.Id, "");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
