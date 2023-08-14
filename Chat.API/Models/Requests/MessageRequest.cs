namespace Chat.API.Models.Requests
{
    public class MessageRequest
    {
        public string conversationId { get; set; }
        public string senderName { get; set; }
        public string receiverId { get; set; }
        public string content { get; set; }
    }
}
