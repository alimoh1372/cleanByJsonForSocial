using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.ChangeProfilePicture;

public class ChangeProfilePictureCommand:IRequest
{
    public long Id { get; set; }
    public byte[] ProfilePicture { get; set; }

    public class Handler:IRequestHandler<ChangeProfilePictureCommand>
    {
        private readonly ISocialNetworkDbContext _context;

        public Handler(ISocialNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ChangeProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.SingleOrDefaultAsync(x => x.UserId == request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            entity.ProfilePicture = request.ProfilePicture;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
            
        }
    } 
}