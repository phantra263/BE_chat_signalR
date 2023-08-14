using AutoMapper;
using Chat.Application.Features.Box.Queries.GetBoxLatestMessage;
using Chat.Application.Features.Box.Queries.GetBoxMessage;
using Chat.Domain.Entities;

namespace Esuhai.HRM.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<GetBoxMessageParameter, GetBoxMessageQuery>();
            CreateMap<Box, GetBoxMessageViewModel>();

            CreateMap<GetBoxLatestMessageParameter, GetBoxLatestMessageQuery>();
            CreateMap<Box, GetBoxLatestMessageViewModel>();
        }
    }
}
