using AutoMapper;
using Chat.Application.Features.Box.Queries.GetBoxChatByUserId;
using Chat.Application.Features.Box.Queries.GetBoxLatestMessage;
using Chat.Application.Features.Box.Queries.GetBoxMessage;
using Chat.Application.Features.User.Commands.Authenticate;
using Chat.Application.Features.User.Commands.Register;
using Chat.Application.Features.User.Queries.GetByNickname;
using Chat.Domain.Entities;

namespace Esuhai.HRM.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<GetByNicknameParameter, GetByNicknameQuery>();

            CreateMap<RegisterParameter, RegisterCommand>();
            CreateMap<User, RegisterViewModel>();

            CreateMap<AuthenticateParameter, AuthenticateCommand>();
            CreateMap<User, AuthenticateViewModel>();

            CreateMap<GetBoxChatByUserIdParameter, GetBoxChatByUserIdQuery>();

            CreateMap<GetBoxMessageParameter, GetBoxMessageQuery>();
            CreateMap<Box, GetBoxMessageViewModel>();

            CreateMap<GetBoxLatestMessageParameter, GetBoxLatestMessageQuery>();
            CreateMap<Box, GetBoxLatestMessageViewModel>();
        }
    }
}
