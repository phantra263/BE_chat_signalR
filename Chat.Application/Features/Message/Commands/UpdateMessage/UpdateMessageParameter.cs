namespace Chat.Application.Features.Message.Commands.UpdateMessage
{
    public class UpdateMessageParameter
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public bool IsSeen { get; set; }
    }
}
