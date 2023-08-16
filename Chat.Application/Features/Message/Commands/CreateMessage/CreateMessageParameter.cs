namespace Chat.Application.Features.Message.Commands.CreateMessage
{
    public class CreateMessageParameter
    {
        public string ConversationId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
    }
}
