using Application.Common.Utility;
using FluentValidation;

namespace Application.Messages.Queries.GetEditMessage;

public class GetEditMessageQueryValidator:AbstractValidator<GetEditMessageQuery>
{
    public GetEditMessageQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName} " + ValidatingMessage.IsRequired);
    }
}