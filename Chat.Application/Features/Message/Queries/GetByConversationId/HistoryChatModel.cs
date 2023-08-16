using System;

namespace Chat.Application.Features.Message.Queries.GetByConversationId
{
    public class HistoryChatModel
    {
        public string id { get; set; }
        public DateTime? created { get; set; }
        public bool? deleted { get; set; }
        public string conversationId { get; set; }
        public string senderId { get; set; }
        public string senderName { get; set; }
        public string receiverId { get; set; }
        public string receiverName { get; set; }
        public string content { get; set; }
        public bool? isSeen { get; set; }
    }
}
