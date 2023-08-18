using FluentValidation;

namespace Chat.Application.Features.User.Commands.UpdateStatusOnline
{
    public class UpdateStatusCommandValidator : AbstractValidator<UpdateStatusOnlineCommand>
    {
        public UpdateStatusCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
