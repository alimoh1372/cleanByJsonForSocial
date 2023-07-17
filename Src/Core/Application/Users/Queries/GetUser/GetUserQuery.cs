using MediatR;

namespace Application.Users.Queries.GetUser;

public class GetUserQuery:IRequest<UserViewModel>
{
    public long Id { get; set; }

}