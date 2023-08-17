using FluentValidation;

namespace Chat.Application.Features.Room.Queries.FindOneAndGetLatestMessage
{
    public class FindOneAndGetLatestMessageQueryValidator : AbstractValidator<FindOneAndGetLatestMessageQuery>
    {
        public FindOneAndGetLatestMessageQueryValidator()
        {
            RuleFor(p => p.RoomId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
