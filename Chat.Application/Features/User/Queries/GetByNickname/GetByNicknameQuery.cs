using Chat.Application.Interfaces.IRepositories;
using Chat.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Features.User.Queries.GetByNickname
{
    public class GetByNicknameQuery : IRequest<PagedResponse<IReadOnlyList<Domain.Entities.User>>>
    {
        public string Nickname { get; set; }
    }

    public class GetByNicknameQueryHandler : IRequestHandler<GetByNicknameQuery, PagedResponse<IReadOnlyList<Domain.Entities.User>>>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public GetByNicknameQueryHandler(IUserRepositoryAsync userRepositoryAsync)
        {
            _userRepositoryAsync = userRepositoryAsync;
        }

        public async Task<PagedResponse<IReadOnlyList<Domain.Entities.User>>> Handle(GetByNicknameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepositoryAsync.GetListByNicknameAsync(request.Nickname);

                return new PagedResponse<IReadOnlyList<Domain.Entities.User>>(users);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
