using Application.Common.Utility;
using FluentValidation;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .MaximumLength(255).WithMessage("{PropertyName}" + ValidatingMessage.MaxLength);

        RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .MaximumLength(255).WithMessage("{PropertyName}" + ValidatingMessage.MaxLength);

        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .MinimumLength(6).WithMessage("{{PropertyName}} Must be greater than {MinLength}")
            .Equal(cp=>cp.ConfirmPassword)
            .WithMessage(u => u.ConfirmPassword.GetType().Name + "Must be equeal to " + u.Password.GetType().Name);
        RuleFor(p => p.AboutMe)
            .NotEmpty().WithMessage("{PropertyName} " + ValidatingMessage.IsRequired)
            .NotNull().WithMessage("{PropertyName} " + ValidatingMessage.IsRequired)
            .MaximumLength(2000).WithMessage("{PropertyName} " + ValidatingMessage.MaxLength + " {MaxLength}");
        RuleFor(u => u.Email.ToString()).EmailAddress()
            .WithMessage("{PropertyName} isn't valid Email Address...");
        RuleFor(x => x.BirthDay)
            .LessThan(DateTime.Now)
            .WithMessage("{PropertyName}"+ApplicationMessage.IsInvalid);
        RuleFor(u => u.AboutMe)
            .MaximumLength(2000)
            .WithMessage("{PropertyName} " + ValidatingMessage.MaxLength + "{MaxLength}");

        RuleFor(u => u.ProfilePicture.Length)
            .LessThanOrEqualTo(3 * 1024)
            .WithMessage("{PropertyName}-" + ValidatingMessage.MaxFileSize +"FileSize={PropertyValue}");
    }
}