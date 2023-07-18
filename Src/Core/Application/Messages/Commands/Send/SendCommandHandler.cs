using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Commands.Send;

public class SendCommandHandler : IRequestHandler<SendCommand, Unit>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IAuthHelper _authHelper;

    public SendCommandHandler(ISocialNetworkDbContext context, IAuthHelper authHelper)
    {
        _context = context;
        _authHelper = authHelper;
    }

    public async Task<Unit> Handle(SendCommand request, CancellationToken cancellationToken)
    {
        //check to deny send message to non-friend users
        if (!await _context.UserRelations.AnyAsync(x =>
                x.FkUserAId == request.FkFromUserId && x.FkUserBId == request.FkToUserId ||
                x.FkUserBId == request.FkFromUserId && x.FkUserAId == request.FkToUserId
                                                    && x.Approve == true, cancellationToken)
           )
            throw new NotAuthorizedRequestException(ApplicationMessage.CantSendToNonFriendUsers);
        var user =await _authHelper.GetUserInfo();


        //Deny to send message instead of other user

        if (user.Id != request.FkFromUserId)
            throw new NotAuthorizedRequestException(
                $"{nameof(request.FkFromUserId)} has invalid id {ApplicationMessage.IsInvalid}"
                );
        
        Message message = new Message()
        {
            FkFromUserId = request.FkFromUserId,
            FkToUserId = request.FkToUserId,
            MessageContent = request.MessageContent
        };

        await _context.Messages.AddAsync(message, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}