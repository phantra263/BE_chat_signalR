using System;

namespace Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom
{
    public class HistoryMessageRoomViewModel
    {
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public string SenderId { get; set; }
        public string AvatarId { get; set; }
        public string AnonymousName { get; set; }
    }
}
