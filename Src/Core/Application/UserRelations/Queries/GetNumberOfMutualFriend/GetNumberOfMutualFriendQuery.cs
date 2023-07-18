using MediatR;

namespace Application.UserRelations.Queries.GetNumberOfMutualFriend;

public record GetNumberOfMutualFriendQuery(long CurrentUserId, long FriendUserId):IRequest<int>;