namespace Chat.Application.Features.User.Commands.Register
{
    public class RegisterViewModel
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string AvatarBgColor { get; set; }
        public bool Status { get; set; }
        public bool IsOnline { get; set; }
    }
}
