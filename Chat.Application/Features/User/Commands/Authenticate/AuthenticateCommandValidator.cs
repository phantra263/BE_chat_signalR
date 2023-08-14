using FluentValidation;

namespace Chat.Application.Features.User.Commands.Authenticate
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(p => p.Nickname)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .Length(1, 100)
                .WithMessage("{PropertyName} must be less than or equal to 100 characters")
                .NotNull();

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .Length(6, 50)
                .WithMessage("{PropertyName} must be from 6 to 50 characters")
                .NotNull();
        }
    }
}
