using Chat.Application.Features.User.Commands.UpdateUserChatRoom;
using FluentValidation;

namespace Chat.Application.Features.User.Commands.UpdateAvatarId
{
    public class UpdateUserChatRoomCommandValidator : AbstractValidator<UpdateUserChatRoomCommand>
    {
        public UpdateUserChatRoomCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
