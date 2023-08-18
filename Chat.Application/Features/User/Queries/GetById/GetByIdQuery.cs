using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.User.Queries.GetById
{
    public class GetByIdQuery : IRequest<Response<Domain.Entities.User>>
    {
        public string Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Response<Domain.Entities.User>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public GetByIdQueryHandler(IUserRepositoryAsync userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<Response<Domain.Entities.User>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            => new Response<Domain.Entities.User>(await _userRepositoryAsync.GetByIdAsync(request.Id));
    }
}
