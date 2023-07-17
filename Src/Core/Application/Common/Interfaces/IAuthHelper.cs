namespace Application.Common.Interfaces;

public interface IAuthHelper
{
  Task<string> CreateToken(AuthViewModel authViewModel);
  Task<AuthViewModel> GetUserInfo();
}