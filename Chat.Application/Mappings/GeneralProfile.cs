using AutoMapper;
using Chat.Application.Features.Message.Queries.GetMessages;

namespace Esuhai.HRM.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<GetMessageParameter, GetMessageQuery>();
        }
    }
}
