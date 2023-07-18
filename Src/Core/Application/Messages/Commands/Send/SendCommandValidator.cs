using Application.Common.Utility;
using Application.UserRelations.Queries.GetNumberOfMutualFriend;
using FluentValidation;

namespace Application.Messages.Commands.Send;

public class SendCommandValidator:AbstractValidator<SendCommand>
{
    public SendCommandValidator()
    {
        RuleFor(x => x.FkToUserId)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName}" + ValidatingMessage.IsRequired)
            .NotEqual(x => x.FkToUserId)
            .WithMessage(x => nameof(GetNumberOfMutualFriendQuery) + " can\'t send message to yourself");
        RuleFor(x => x.FkFromUserId)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(long.MaxValue)
            .WithMessage("{PropertyName}" + ValidatingMessage.IsRequired);

        RuleFor(x => x.MessageContent)
            .NotEmpty()
            .NotNull().WithMessage("{PropertyName}" + ValidatingMessage.IsRequired);
    }
}