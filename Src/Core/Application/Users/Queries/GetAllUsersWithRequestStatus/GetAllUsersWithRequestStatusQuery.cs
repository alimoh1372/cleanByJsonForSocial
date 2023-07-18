using MediatR;

namespace Application.Users.Queries.GetAllUsersWithRequestStatus;

public record GetAllUsersWithRequestStatusQuery() : IRequest<UserWithRequestStatusListVm>;
