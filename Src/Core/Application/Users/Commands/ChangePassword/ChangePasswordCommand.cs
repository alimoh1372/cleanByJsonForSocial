using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.ChangePassword;

public class ChangePasswordCommand:IRequest
{
    public long Id { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public class Handler:IRequestHandler<ChangePasswordCommand>
    {
        private readonly ISocialNetworkDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(ISocialNetworkDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var entity =await _context.Users.SingleOrDefaultAsync(x => x.UserId == request.Id, cancellationToken: cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var password = _passwordHasher.Hash(request.Password);

            entity.Password = password;

           await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}