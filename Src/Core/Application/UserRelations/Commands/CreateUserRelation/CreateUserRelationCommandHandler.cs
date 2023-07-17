using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UserRelations.Commands.CreateUserRelation;

public class CreateUserRelationCommandHandler:IRequestHandler<CreateUserRelationCommand,Unit>
{
    private readonly ISocialNetworkDbContext _context;


    public CreateUserRelationCommandHandler(ISocialNetworkDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CreateUserRelationCommand request,CancellationToken cancellationToken)
    {
        var userRelation = new UserRelation
        {

            FkUserAId = request.FkUserAId,
            FkUserBId = request.FkUserBId,
            RelationRequestMessage = request.RelationRequestMessage,
        };

        //check Duplication of request
        if (_context.UserRelations.Any(x =>
                x.FkUserAId == request.FkUserAId && x.FkUserBId == request.FkUserBId ||
                x.FkUserBId == request.FkUserBId && x.FkUserAId == request.FkUserAId))
            throw new DuplicationException(nameof(userRelation),
                $"UserIds:User A={request.FkUserAId} and User B={request.FkUserBId}");
        await _context.UserRelations.AddAsync(userRelation, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
        
    }
}