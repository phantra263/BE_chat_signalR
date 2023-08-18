using Chat.Application.Parameters;

namespace Chat.Application.Features.Room.Queries.FindAnyAndGetLatestMessage
{
    public class FindAnyAndGetLatestMessageParameter : RequestParameter
    {
        public string Keyword { get; set; }
    }
}
