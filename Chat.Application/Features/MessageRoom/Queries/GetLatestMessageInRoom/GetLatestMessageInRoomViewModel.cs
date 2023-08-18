using System.Collections.Generic;

namespace Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom
{
    public class GetLatestMessageInRoomViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public IReadOnlyList<HistoryMessageRoomViewModel> Messages { get; set; }
    }
}
