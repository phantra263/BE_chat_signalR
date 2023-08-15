using Chat.Application.Parameters;

namespace Chat.Application.Features.Message.Queries.GetByConversationId
{
    public class GetByConversationIdParameter : RequestParameter
    {
        public string Keyword { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
    }
}
