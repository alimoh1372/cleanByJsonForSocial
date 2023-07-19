using Application.Common.Utility;
using FluentValidation;

namespace Application.Messages.Queries.GetMessage;

public class GetMessageQueryValidator:AbstractValidator<GetMessageQuery>
{
    public GetMessageQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .LessThan(long.MaxValue)
            .WithMessage("{PropertyName} " + ValidatingMessage.IsRequired);
    }
}