namespace Chat.Application.Features.User.Commands.Authenticate
{
    public class AuthenticateViewModel
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string AvatarBgColor { get; set; }
        public bool Status { get; set; }
        public bool IsOnline { get; set; }
    }
}
