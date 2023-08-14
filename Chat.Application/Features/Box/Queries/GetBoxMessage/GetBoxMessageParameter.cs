using Chat.Application.Parameters;

namespace Chat.Application.Features.Box.Queries.GetBoxMessage
{
    public class GetBoxMessageParameter : RequestParameter
    {
        public string Keyword { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
