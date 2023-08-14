namespace Chat.Application.Features.Box.Queries.GetBoxLatestMessage
{
    public class GetBoxLatestMessageViewModel
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public bool IsLock { get; set; }
        public bool IsMute { get; set; }
        public Domain.Entities.Message Message { get; set; }
    }
}
