using FluentValidation;

namespace Chat.Application.Features.MessageRoom.Commands.CreateMessageRoom
{
    public class CreateMessageRoomCommandValidator : AbstractValidator<CreateMessageRoomCommand>
    {
        public CreateMessageRoomCommandValidator()
        {
            RuleFor(p => p.Content)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.SenderId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.RoomId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
