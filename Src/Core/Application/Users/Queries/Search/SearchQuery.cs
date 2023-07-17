using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Queries.Search;

/// <summary>
/// Get the information of user
/// </summary>
/// <param name="Email"></param>
/// <returns></returns>
public class SearchQuery:IRequest<UserListVm>
{
    public string Email { get; set; }
}