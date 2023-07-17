using Application.Common.Utility;
using FluentValidation;

namespace Application.UserRelations.Commands.AcceptUserRelation;

public class AcceptUserRelationCommandValidator:AbstractValidator<AcceptUserRelationCommand>
{
    public AcceptUserRelationCommandValidator()
    {
        RuleFor(x=>x.UserIdRequestSentToIt)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .NotEqual(x => x.UserIdRequestSentFromIt)
            .WithMessage(x => nameof(x) + ApplicationMessage.CantSelfRequest);
        
    }  
}