using Application.Common.Utility;
using FluentValidation;

namespace Application.Messages.Queries.GetMessages;

public class GetMessageQueryValidator:AbstractValidator<GetMessagesQuery>
{
    public GetMessageQueryValidator()
    {
        RuleFor(x => x.IdUserACurrentUser)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .NotEqual(x => x.IdUserB)
            .WithMessage(x => nameof(x) + ApplicationMessage.CantSelfRequest);

        
    }
}