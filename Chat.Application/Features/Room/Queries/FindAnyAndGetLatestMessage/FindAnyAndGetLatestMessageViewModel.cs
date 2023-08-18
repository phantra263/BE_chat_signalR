using System;

namespace Chat.Application.Features.Room.Queries.FindAnyAndGetLatestMessage
{
    public class FindAnyAndGetLatestMessageViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool? status { get; set; }
        public string userId { get; set; }
        public DateTime? createdRoom { get; set; }

        // latest message in room
        public string content { get; set; }
        public string senderId { get; set; }
        public DateTime? created { get; set; }
    }
}
