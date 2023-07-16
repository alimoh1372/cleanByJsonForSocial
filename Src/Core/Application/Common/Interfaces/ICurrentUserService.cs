namespace Application.Common.Interfaces;


/// <summary>
/// Get the current user information
/// </summary>
public interface ICurrentUserService
{

    string UserId { get; }

    bool IsAuthenticated { get; }

}