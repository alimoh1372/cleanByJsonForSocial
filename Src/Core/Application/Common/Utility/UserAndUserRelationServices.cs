using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Users.Queries.GetAllUsersWithRequestStatus;
using Domain.Entities;

namespace Application.Common.Utility;

public class UserAndUserRelationServices:IUserAndUserRelationService
{
    

    public  async Task<List<UserWithRequestStatusVm>> FillAllOtherUserWithRequestProperties(
        IList<UserWithRequestStatusVm> userWithRequestStatuses, List<UserRelation> userRelations, long currentUserId,
        CancellationToken cancellationToken)
    {
        List<UserWithRequestStatusVm> users = userWithRequestStatuses.ToList();
        Parallel.For(0, users.Count(),  i =>
        {
            var user = users[i];
            user.RequestStatusNumber =CheckStatusOfRequest(userRelations,currentUserId, user.UserId).Result;
            user.MutualFriendNumber = GetMutualFriendNumber(userRelations, currentUserId, user.UserId).Result;
            user.RelationRequestMessage = GetRequestMessage(userRelations,currentUserId, user.UserId).Result;
        });
        return await Task.FromResult(users);
    }

    public Task<RequestStatus> CheckStatusOfRequest(List<UserRelation> userRelations,long userIdA, long userIdB)
    {
        //get relation between a to b or inversion of it
        var _userRelations =  userRelations.Where(x => (x.FkUserAId == userIdA && x.FkUserBId == userIdB)
                                                            || (x.FkUserAId == userIdB && x.FkUserBId == userIdA))
            .ToList();

        //check if isn't any request return that status
        if (userRelations.Count == 0)
            return Task.FromResult(RequestStatus.WithoutRequest); 
        //if relations is bigger than 1 there is a error in application logic
        if (userRelations.Count > 1)
            return Task.FromResult(RequestStatus.ErrorWithRelationNumbers);

        var userRelation = userRelations.First();


        if (userRelation.FkUserAId == userIdA && userRelation.FkUserBId == userIdB && userRelation.Approve == false)
            return Task.FromResult(RequestStatus.RequestPending);

        if (userRelation.FkUserAId == userIdA && userRelation.FkUserBId == userIdB && userRelation.Approve == true)
            return Task.FromResult(RequestStatus.RequestAccepted);

        if (userRelation.FkUserAId == userIdB && userRelation.FkUserBId == userIdA && userRelation.Approve == false)
            return Task.FromResult(RequestStatus.RevertRequestPending);

        if (userRelation.FkUserAId == userIdB && userRelation.FkUserBId == userIdA && userRelation.Approve == true)
            return Task.FromResult(RequestStatus.RevertRequestAccepted);

        return Task.FromResult(RequestStatus.UnknownError);
    }

    public Task<int> GetMutualFriendNumber(List<UserRelation> userRelations,long currentUserId, long friendUserId)
    {
        #region Way1Ofsolve this

        ////Get a list of friendShip of friend with user id friendUserId
        //var listOfUserIdFriendShip = await _context.UserRelations.Where(x => x.Approve && (x.FkUserAId == friendUserId || x.FkUserBId == friendUserId))
        //    .ToListAsync();

        ////Get a list of friendShip of current User
        //var listOfCurrentUserIdFriendShip = await _context.UserRelations.Where(x =>
        //    x.Approve && (currentUserId == x.FkUserAId || currentUserId == x.FkUserBId)).ToListAsync();


        //int mutualFriends = (int)0;
        //Parallel.ForEach(listOfUserIdFriendShip, (userRelation) =>
        //{
        //    //Find the friend of a friend id to check it is also friend with current user?
        //    var otherFriendId = userRelation.FkUserAId == friendUserId ? userRelation.FkUserBId : userRelation.FkUserAId;


        //    //Check There is any relation between current user and other friend of current user's friend
        //    bool isExistAFriendShipWithCurrentUser =
        //        listOfCurrentUserIdFriendShip.Any(x =>
        //            x.FkUserAId == otherFriendId || x.FkUserBId == otherFriendId);

        //    //Add number of friend
        //    if (isExistAFriendShipWithCurrentUser)
        //    {
        //        mutualFriends++;
        //    }
        //});
        //return mutualFriends;

        #endregion

        #region OtherWayToSolveThisProblem
        //Get List of id of friend of current user id
        var currentUserFriendsId = userRelations.Where(x =>
            x.Approve && (currentUserId == x.FkUserAId || currentUserId == x.FkUserBId))
            .Select(x => new
            {
                Id = x.FkUserAId == currentUserId ? x.FkUserBId : x.FkUserAId
            });
        var userIdFriendsIs = userRelations.Where(x =>
                x.Approve && (friendUserId == x.FkUserAId || friendUserId == x.FkUserBId))
            .Select(x => new
            {
                Id = x.FkUserAId == friendUserId ? x.FkUserBId : x.FkUserAId
            });
        //Find Count of common id in two list
        var countMutualFriend =  currentUserFriendsId.Intersect(userIdFriendsIs).Count();
        return Task.FromResult(countMutualFriend);

        #endregion
    }

    public Task<string> GetRequestMessage(List<UserRelation> userRelations,long userIdA, long userIdB)
    {
        //get relation between a to b or inversion of it
        var userRelation =  userRelations
            .FirstOrDefault(x => (x.FkUserAId == userIdA && x.FkUserBId == userIdB)
                                 || (x.FkUserAId == userIdB && x.FkUserBId == userIdA));
        return Task.FromResult(userRelation == null ? string.Empty : userRelation.RelationRequestMessage);
    }
}