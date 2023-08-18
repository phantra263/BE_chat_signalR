using AutoMapper;
using Chat.Application.Features.Box.Commands.CreateBoxLatestMessage;
using Chat.Application.Features.Box.Queries.GetBoxChatByUserId;
using Chat.Application.Features.Box.Queries.GetBoxMessage;
using Chat.Application.Features.Box.Queries.GetBoxSelected;
using Chat.Application.Features.Message.Commands.CreateMessage;
using Chat.Application.Features.Message.Commands.UpdateMessage;
using Chat.Application.Features.Message.Queries.GetByConversationId;
using Chat.Application.Features.MessageRoom.Commands.CreateMessageRoom;
using Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom;
using Chat.Application.Features.Room.Commands.CreateRoom;
using Chat.Application.Features.Room.Queries.FindAnyAndGetLatestMessage;
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
            #region User
            CreateMap<GetByNicknameParameter, GetByNicknameQuery>();
            CreateMap<RegisterParameter, RegisterCommand>();
            CreateMap<User, RegisterViewModel>();
            CreateMap<AuthenticateParameter, AuthenticateCommand>();
            CreateMap<User, AuthenticateViewModel>();
            #endregion

            #region Box
            CreateMap<GetBoxChatByUserIdParameter, GetBoxChatByUserIdQuery>();
            CreateMap<GetBoxMessageParameter, GetBoxMessageQuery>();
            CreateMap<Box, GetBoxMessageViewModel>();
            CreateMap<Box, CreateBoxLatestMessageViewModel>();
            CreateMap<GetBoxSelectedParameter, GetBoxSelectedQuery>();
            CreateMap<Box, GetBoxSelectedViewModel>();
            #endregion

            #region Message
            CreateMap<GetByConversationIdParameter, GetByConversationIdQuery>();
            CreateMap<CreateMessageParameter, CreateMessageCommand>();
            CreateMap<Message, CreateMessageViewModel>();
            CreateMap<UpdateMessageParameter, UpdateMessageCommand>();
            #endregion

            #region Room
            CreateMap<CreateRoomParameter, CreateRoomCommand>();
            CreateMap<CreateRoomCommand, Room>();
            CreateMap<Room, CreateRoomViewModel>();

            CreateMap<FindAnyAndGetLatestMessageParameter, FindAnyAndGetLatestMessageQuery>();
            #endregion

            #region MessageRoom
            CreateMap<GetLatestMessageInRoomParameter, GetLatestMessageInRoomQuery>();
            CreateMap<Room, GetLatestMessageInRoomViewModel>();

            CreateMap<CreateMessageCommand, MessageRoom>();
            CreateMap<CreateMessageRoomParameter, CreateMessageRoomCommand>();
            #endregion
        }
    }
}
