namespace Chat.API.Models.Requests
{
    public class ReadMessageRequest
    {
        public string ReceiverId { get; set; }
        public bool IsSeen { get; set; }
    }
}
