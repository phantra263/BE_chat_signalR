namespace Lab.SignalR_Chat.BE.Models
{
    public class ReadMessageRequest
    {
        public string receiverId { get; set; }
        public bool seen { get; set; }
    }
}
