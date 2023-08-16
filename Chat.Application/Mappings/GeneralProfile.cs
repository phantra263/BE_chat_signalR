using AutoMapper;
using Chat.Application.Features.Box.Queries.GetBoxChatByUserId;
using Chat.Application.Features.Box.Queries.GetBoxMessage;
using Chat.Application.Features.Box.Queries.GetBoxSelected;
using Chat.Application.Features.Message.Commands.CreateMessage;
using Chat.Application.Features.Message.Commands.UpdateMessage;
using Chat.Application.Features.Message.Queries.GetByConversationId;
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

            CreateMap<GetBoxSelectedParameter, GetBoxSelectedQuery>();
            CreateMap<Box, GetBoxSelectedViewModel>();

            CreateMap<GetByConversationIdParameter, GetByConversationIdQuery>();

            CreateMap<CreateMessageParameter, CreateMessageCommand>();
            CreateMap<Message, CreateMessageViewModel>();

            CreateMap<UpdateMessageParameter, UpdateMessageCommand>();
        }
    }
}
