using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.Box.Commands.CreateBox
{
    public class CreateBoxCommand : IRequest<Response<Domain.Entities.Box>>
    {
        public string User1Id { get; set; }
        public string User2Id { get; set; }
    }

    public class CreateBoxCommandHandler : IRequestHandler<CreateBoxCommand, Response<Domain.Entities.Box>>
    {
        private readonly IBoxRepositoryAsync _boxRepositoryAsync;

        public CreateBoxCommandHandler(IBoxRepositoryAsync boxRepositoryAsync)
        {
            _boxRepositoryAsync = boxRepositoryAsync;
        }

        public async Task<Response<Domain.Entities.Box>> Handle(CreateBoxCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var boxExist = await _boxRepositoryAsync.GetCheckExist(request.User1Id, request.User2Id);
                if (boxExist != null)
                    return new Response<Domain.Entities.Box>("Đã tồn tại cuộc hội thoại này");

                var box = await _boxRepositoryAsync.CreateAsync(new Domain.Entities.Box
                {
                    Created = DateTime.Now,
                    IsLock = false,
                    IsMute = false,
                    User1Id = request.User1Id,
                    User2Id = request.User2Id
                });

                return new Response<Domain.Entities.Box>(box);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
