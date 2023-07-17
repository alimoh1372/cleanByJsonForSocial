using Application.Common.Utility;
using FluentValidation;

namespace Application.UserRelations.Commands.CreateUserRelation;

public class CreateUserRelationCommandValidator:AbstractValidator<CreateUserRelationCommand>
{
    public CreateUserRelationCommandValidator()
    {
        RuleFor(c => c.FkUserBId)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .NotEqual(x => x.FkUserBId)
            .WithMessage(x => nameof(x) + ApplicationMessage.CantSelfRequest);
        RuleFor(x => x.RelationRequestMessage)
            .MaximumLength(100)
            .WithMessage("{PropertyName}" + ValidatingMessage.MaxLength + " {PropertyValue}");

    }
}