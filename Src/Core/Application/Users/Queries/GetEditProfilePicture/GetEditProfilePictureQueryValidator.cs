using Application.Common.Utility;
using FluentValidation;

namespace Application.Users.Queries.GetEditProfilePicture;

public class GetEditProfilePictureQueryValidator:AbstractValidator<GetEditProfilePictureQuery>
{
    public GetEditProfilePictureQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName} " + ValidatingMessage.IsRequired);
    }
}