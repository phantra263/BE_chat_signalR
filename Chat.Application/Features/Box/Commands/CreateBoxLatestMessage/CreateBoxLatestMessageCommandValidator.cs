using FluentValidation;

namespace Chat.Application.Features.Box.Commands.CreateBoxLatestMessage
{
    public class CreateBoxLatestMessageCommandValidator : AbstractValidator<CreateBoxLatestMessageCommand>
    {
        public CreateBoxLatestMessageCommandValidator()
        {
            RuleFor(p => p.SenderId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ReceiverId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}

