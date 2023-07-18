using Application.Common.Utility;
using FluentValidation;

namespace Application.UserRelations.Queries.GetNumberOfMutualFriend;

public class GetNumberOfMutualFriendQueryValidator:AbstractValidator<GetNumberOfMutualFriendQuery>
{
    public GetNumberOfMutualFriendQueryValidator()
    {
        RuleFor(x => x.CurrentUserId)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .NotEqual(x => x.FriendUserId)
            .WithMessage(x => nameof(GetNumberOfMutualFriendQuery) + ApplicationMessage.IsInvalid);
        RuleFor(x => x.FriendUserId)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName}" + ValidatingMessage.IsRequired);
    }
}