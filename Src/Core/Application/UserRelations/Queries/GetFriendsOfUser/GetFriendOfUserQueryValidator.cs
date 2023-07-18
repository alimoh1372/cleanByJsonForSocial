using Application.Common.Utility;
using Application.Users.Queries.GetEditProfilePicture;
using FluentValidation;

namespace Application.UserRelations.Queries.GetFriendsOfUser;

public class GetFriendOfUserQueryValidator:AbstractValidator<GetFriendsOfUserQuery>
{
    public GetFriendOfUserQueryValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName} " + ValidatingMessage.IsRequired);
    }
}