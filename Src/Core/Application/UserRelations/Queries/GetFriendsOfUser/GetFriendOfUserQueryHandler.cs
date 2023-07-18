using System.Linq;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Users.Queries.GetAllUsersWithRequestStatus;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserRelations.Queries.GetFriendsOfUser;

public class GetFriendOfUserQueryHandler : IRequestHandler<GetFriendsOfUserQuery, UserWithRequestStatusListVm>
{
    private readonly ISocialNetworkDbContext _context;

    public GetFriendOfUserQueryHandler(ISocialNetworkDbContext context)
    {
        _context = context;
    }

    public async Task<UserWithRequestStatusListVm> Handle(GetFriendsOfUserQuery request,
        CancellationToken cancellationToken)
    {
        return new UserWithRequestStatusListVm
        {
            Users = await _context.UserRelations
                .Include(x => x.UserA)
                .Include(x => x.UserB)
                //Filter All relation that accepted and user with id=friendUserId participate in it
                .Where(x => (x.UserA.UserId == request.UserId || x.UserB.UserId == request.UserId) && x.Approve)
                .Select(x => new UserWithRequestStatusVm()
                {
                    //Set the userWith relation status to show the other users that friend with him and show other users in relation info
                    UserId = x.FkUserAId == request.UserId ? x.FkUserBId : x.FkUserAId,
                    Name = x.FkUserAId == request.UserId ? x.UserB.Name : x.UserA.Name,
                    LastName = x.FkUserAId == request.UserId ? x.UserB.LastName : x.UserA.LastName,
                    //show that this user(otherUser) is requested the relation or not
                    RequestStatusNumber = x.FkUserAId == request.UserId
                        ? RequestStatus.RequestAccepted
                        : RequestStatus.RevertRequestAccepted,
                    TimeOffset = x.Created,
                    ProfilePicture = x.FkUserAId == request.UserId ? x.UserB.ProfilePicture : x.UserA.ProfilePicture,
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken)
        };
    }
}