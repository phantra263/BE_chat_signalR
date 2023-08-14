using FluentValidation;

namespace Chat.Application.Features.Box.Queries.GetBoxLatestMessage
{
    public class GetBoxLatestMessageQueryValidator : AbstractValidator<GetBoxLatestMessageQuery>
    {
        public GetBoxLatestMessageQueryValidator()
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
