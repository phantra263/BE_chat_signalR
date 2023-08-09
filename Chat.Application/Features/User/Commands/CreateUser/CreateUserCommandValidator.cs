using FluentValidation;

namespace Chat.Application.Features.User.Commands.CreateUser
{
    class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(p => p.Nickname)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .Length(1, 100)
                .WithMessage("{PropertyName} must be less than or equal to 100 characters")
                .NotNull();
        }
    }
}
