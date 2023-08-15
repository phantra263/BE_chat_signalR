using System.Collections.Generic;

namespace Chat.Application.Features.Message.Queries.GetByConversationId
{
    public class GetByConversationIdViewModel
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string AvatarBgColor { get; set; }
        public bool Status { get; set; }
        public bool IsOnline { get; set; }
        public IReadOnlyList<HistoryChatModel> Messages { get; set; }
    }
}
