using System;

namespace Chat.Application.DTOs.SignalDtos
{
    public class ReceiveMessageRoomResponse
    {
        public string RoomId { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public string SenderId { get; set; }
        public string AvatarId { get; set; }
        public string AnonymousName { get; set; }
    }
}
