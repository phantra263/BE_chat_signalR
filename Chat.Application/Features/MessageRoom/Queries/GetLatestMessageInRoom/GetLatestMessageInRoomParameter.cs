using Chat.Application.Parameters;

namespace Chat.Application.Features.MessageRoom.Queries.GetLatestMessageInRoom
{
    public class GetLatestMessageInRoomParameter : RequestParameter
    {
        public string Keyword { get; set; }
        public string RoomId { get; set; }
    }
}
