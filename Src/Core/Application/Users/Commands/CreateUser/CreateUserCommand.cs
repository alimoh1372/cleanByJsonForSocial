using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name,
    string LastName, Email Email, DateTime BirthDay
    , string Password, string ConfirmPassword, string AboutMe,
    byte[] ProfilePicture) : IRequest<Unit>;




