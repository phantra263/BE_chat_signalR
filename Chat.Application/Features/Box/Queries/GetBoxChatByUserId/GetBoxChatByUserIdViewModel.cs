namespace Chat.Application.Features.Box.Queries.GetBoxChatByUserId
{
    public class GetBoxChatByUserIdViewModel
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public bool IsLock { get; set; }
        public bool IsMute { get; set; }

        public string UserId { get; set; }
        public string Nickname { get; set; }
        public string AvatarBgColor { get; set; }
        public bool Status { get; set; }
        public bool IsOnline { get; set; }

        public string Content { get; set; }
        public bool IsSeen { get; set; }
    }
}
