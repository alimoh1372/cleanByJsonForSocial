using Application.Common.Utility;
using FluentValidation;

namespace Application.Messages.Commands.Edit;

public class EditCommandValidator:AbstractValidator<EditCommand>
{
    public EditCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName} " + ValidatingMessage.IsRequired);

        RuleFor(x => x.MessageContent)
            .NotEmpty()
            .NotNull().WithMessage("{PropertyName}" + ValidatingMessage.IsRequired);
    }
}