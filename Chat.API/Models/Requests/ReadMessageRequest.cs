namespace Chat.API.Models.Requests
{
    public class ReadMessageRequest
    {
        public string receiverId { get; set; }
        public bool seen { get; set; }
    }
}
