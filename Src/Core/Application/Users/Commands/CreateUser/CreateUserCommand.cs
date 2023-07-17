using System.Security.Cryptography.X509Certificates;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.CreateUser;

    public class CreateCommandRecordHandler:IRequestHandler<CreateCommandRecord>
    {
        private readonly ISocialNetworkDbContext _context;
        private readonly IMediator _mediator;
        private readonly IPasswordHasher _passwordHasher;

        public CreateCommandRecordHandler(ISocialNetworkDbContext context, IMediator mediator, IPasswordHasher passwordHasher)
        {
            _context = context;
            _mediator = mediator;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(CreateCommandRecord request, CancellationToken cancellationToken)
        {
            var password = _passwordHasher.Hash(request.Password);
            var entity = new User
            {

                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                BirthDay = request.BirthDay,
                Password = password,
                AboutMe = request.AboutMe,
                ProfilePicture = request.ProfilePicture
            };
            if (await _context.Users.AnyAsync(x => x.Email.ToString() == request.Email.ToString(), cancellationToken))
                throw new DuplicationException(request.Email.GetType().Name,request.Email.ToString());
        await   _context.Users.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new UserCreatedNotification { UserId = entity.UserId.ToString() }, cancellationToken);
        return Unit.Value;
        }
    }
//}