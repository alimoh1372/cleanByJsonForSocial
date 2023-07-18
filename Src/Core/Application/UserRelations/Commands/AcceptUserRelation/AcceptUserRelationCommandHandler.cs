using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRelations.Commands.AcceptUserRelation;

public class AcceptUserRelationCommandHandler:IRequestHandler<AcceptUserRelationCommand,Unit>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IAuthHelper _authHelper;

    public AcceptUserRelationCommandHandler(ISocialNetworkDbContext context, IAuthHelper authHelper)
    {
        _context = context;
        _authHelper = authHelper;
    }

    public async Task<Unit> Handle(AcceptUserRelationCommand request,CancellationToken cancellationToken)
    {

        //Handle User exception
        var authenticatedUser =await _authHelper.GetUserInfo();
        if (authenticatedUser == null)
            throw new NotAuthenticatedRequestException();
        //Handle the authorization 
        if (authenticatedUser.Id != request.UserIdRequestSentToIt)
            throw new NotAuthorizedRequestException();

        var relation = await _context.UserRelations.FirstOrDefaultAsync(x =>
            x.FkUserAId == request.UserIdRequestSentFromIt && x.FkUserBId == request.UserIdRequestSentToIt
            ,cancellationToken);
        if (relation == null)
            throw new NotFoundException(nameof(UserRelation)
                , $"{nameof(request.UserIdRequestSentToIt)},{nameof(request.UserIdRequestSentFromIt)}");

        relation.Approve = true;
       await _context.SaveChangesAsync(cancellationToken);

       return Unit.Value;
    }
}