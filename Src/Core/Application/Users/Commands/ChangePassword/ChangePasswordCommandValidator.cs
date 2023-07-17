using Application.Common.Interfaces;
using Application.Common.Utility;
using FluentValidation;

namespace Application.Users.Commands.ChangePassword;

public class ChangePasswordCommandValidator:AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator(ISocialNetworkDbContext context)
    {

        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName} " + ValidatingMessage.IsRequired);

        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .MinimumLength(6).WithMessage("{{PropertyName}} Must be greater than {MinLength}")
            .MaximumLength(30).WithMessage("{PropertyName}"+ ValidatingMessage.MaxLength +"{MaxLength}")
            .Equal(cp => cp.ConfirmPassword)
            .WithMessage(u => u.ConfirmPassword.GetType().Name + "Must be equal to " + u.Password.GetType().Name);
        
    }
}