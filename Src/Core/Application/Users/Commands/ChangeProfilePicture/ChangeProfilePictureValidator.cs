using Application.Common.Utility;
using FluentValidation;

namespace Application.Users.Commands.ChangeProfilePicture;

public class ChangeProfilePictureValidator:AbstractValidator<ChangeProfilePictureCommand>
{
    public ChangeProfilePictureValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName} " + ValidatingMessage.IsRequired);
        RuleFor(u => u.ProfilePicture.Length)
            .LessThanOrEqualTo(3 * 1024)
            .WithMessage("{PropertyName}-" + ValidatingMessage.MaxFileSize + "FileSize={PropertyValue}");
    }
}