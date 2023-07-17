using Application.Users.Queries.GetUser;

namespace Application.Users.Queries.Search;

public class UserListVm
{
    public IList<UserViewModel> Users { get; set; }
}