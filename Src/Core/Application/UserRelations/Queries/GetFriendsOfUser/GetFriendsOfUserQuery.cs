using Application.Users.Queries.GetAllUsersWithRequestStatus;
using MediatR;

namespace Application.UserRelations.Queries.GetFriendsOfUser;

public record GetFriendsOfUserQuery(long UserId):IRequest<UserWithRequestStatusListVm>;