using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRelations.Queries.GetNumberOfMutualFriend;

public class GetNumberOfMutualFriendQueryHandler:IRequestHandler<GetNumberOfMutualFriendQuery,int>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IUserAndUserRelationService _userAndUserRelationService;


    public GetNumberOfMutualFriendQueryHandler(ISocialNetworkDbContext context, IUserAndUserRelationService userAndUserRelationService)
    {
        _context = context;
        _userAndUserRelationService = userAndUserRelationService;
    }

    public async Task<int> Handle(GetNumberOfMutualFriendQuery request,CancellationToken cancellationToken)
    {
        var userRelations =await _context.UserRelations.ToListAsync(cancellationToken);
        if (!userRelations.Any())
            return 0;

        return await _userAndUserRelationService.GetMutualFriendNumber(userRelations, request.CurrentUserId,
            request.FriendUserId);
    }
}