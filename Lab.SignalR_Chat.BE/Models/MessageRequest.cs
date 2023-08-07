using System;

namespace Lab.SignalR_Chat.BE.Models
{
    public class MessageRequest
    {
        public string ConversationId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public DateTime? Timming { get; set; }
    }
}
