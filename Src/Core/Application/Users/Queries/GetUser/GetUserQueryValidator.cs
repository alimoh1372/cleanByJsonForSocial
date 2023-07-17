using Application.Common.Utility;
using FluentValidation;

namespace Application.Users.Queries.GetUser;

public class GetUserQueryValidator:AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName} " + ValidatingMessage.IsRequired);
    }
}