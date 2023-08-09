using System;

namespace Lab.SignalR_Chat.BE.Models.Requests
{
    public class MessageRequest
    {
        public string id { get; set; }
        public string conversationId { get; set; }
        public string senderName { get; set; }
        public string receiverId { get; set; }
        public string receiverName { get; set; }
        public string content { get; set; }
        public DateTime? timming { get; set; }
        public bool seen { get; set; }
    }
}
