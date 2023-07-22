using Application.Common.Models;

namespace Application.Common.Interfaces;


/// <summary>
/// An abstraction for helping the authenticating and creating token
/// <remarks>
/// Get the current user information <br/>
/// Get the status of user that authenticated or not <br/>
/// Get the token based on user information and jwt setting on appSetting.json
/// </remarks>
/// </summary>
public interface IAuthHelper
{
  Task<string> CreateToken(AuthViewModel authViewModel);
  Task<AuthViewModel> GetUserInfo();

  Task<bool> IsAuthenticated();
}