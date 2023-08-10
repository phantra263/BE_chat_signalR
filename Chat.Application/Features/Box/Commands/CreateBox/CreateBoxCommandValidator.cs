using FluentValidation;

namespace Chat.Application.Features.Box.Commands.CreateBox
{
    public class CreateBoxCommandValidator : AbstractValidator<CreateBoxCommand>
    {
        public CreateBoxCommandValidator()
        {
            RuleFor(p => p.User1Id)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.User2Id)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
