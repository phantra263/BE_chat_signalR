using FluentValidation;

namespace Chat.Application.Features.Box.Queries.GetBoxMessage
{
    public class GetBoxMessageQueryValidator : AbstractValidator<GetBoxMessageQuery>
    {
        public GetBoxMessageQueryValidator()
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
