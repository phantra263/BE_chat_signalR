using System;

namespace Chat.Application.Features.Message.Commands.CreateMessage
{
    public class CreateMessageViewModel
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public bool IsSeen { get; set; }
    }
}
