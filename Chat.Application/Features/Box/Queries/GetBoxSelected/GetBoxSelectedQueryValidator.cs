using FluentValidation;

namespace Chat.Application.Features.Box.Queries.GetBoxSelected
{
    public class GetBoxSelectedQueryValidator : AbstractValidator<GetBoxSelectedQuery>
    {
        public GetBoxSelectedQueryValidator()
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
