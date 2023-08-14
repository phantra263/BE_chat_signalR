using Chat.Application.Parameters;

namespace Chat.Application.Features.Box.Queries.GetBoxChatByUserId
{
    public class GetBoxChatByUserIdParameter : RequestParameter
    {
        public string Keyword { get; set; }
        public string UserId { get; set; }
    }
}
