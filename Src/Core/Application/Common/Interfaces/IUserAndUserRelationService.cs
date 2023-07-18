using Application.Common.Models;
using Application.Users.Queries.GetAllUsersWithRequestStatus;
using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUserAndUserRelationService
{
    Task<List<UserWithRequestStatusVm>> FillAllOtherUserWithRequestProperties(
        IList<UserWithRequestStatusVm> userWithRequestStatuses, List<UserRelation> userRelations, long currentUserId,
        CancellationToken cancellationToken);
    Task<RequestStatus> CheckStatusOfRequest(List<UserRelation> userRelations, long userIdA, long userIdB);
    Task<int> GetMutualFriendNumber(List<UserRelation> userRelations, long currentUserId, long friendUserId);
    Task<string> GetRequestMessage(List<UserRelation> userRelations, long userIdA, long userIdB);
}