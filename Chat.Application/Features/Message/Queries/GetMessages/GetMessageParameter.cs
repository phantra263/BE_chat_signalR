using Chat.Application.Parameters;

namespace Chat.Application.Features.Message.Queries.GetMessages
{
    public class GetMessageParameter : RequestParameter
    {
        public string Keyword { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
