using System.Collections.Generic;

namespace Chat.Application.Features.Box.Queries.GetBoxMessage
{
    public class GetBoxMessageViewModel
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public bool IsLock { get; set; }
        public bool IsMute { get; set; }
        public IReadOnlyList<Domain.Entities.Message> Messages { get; set; }
    }
}
