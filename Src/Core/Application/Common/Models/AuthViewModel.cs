namespace Application.Common.Models;

/// <summary>
/// A model to using to authenticate
/// </summary>
public class AuthViewModel
{
    public long Id { get; set; }
    public string Username { get; set; }
    public AuthViewModel()
    {
    }

    public AuthViewModel(long id, string username)
    {
        Id = id;
        Username = username;
    }

}