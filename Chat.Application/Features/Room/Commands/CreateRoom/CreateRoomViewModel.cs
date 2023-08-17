using System;

namespace Chat.Application.Features.Room.Commands.CreateRoom
{
    public class CreateRoomViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string UserId { get; set; }

        // latest message in room
        public string Content { get; set; }
        public string SenderId { get; set; }
        public DateTime? Created { get; set; }
    }
}
