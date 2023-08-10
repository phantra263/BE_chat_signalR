namespace Chat.Application.Features.Message.Commands.CreateMessage
{
    public class CreateMessageParameter
    {
        public string ConversationId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public bool IsSeen { get; set; }
    }
}
