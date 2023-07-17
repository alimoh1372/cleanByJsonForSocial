using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand:IRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public Email Email { get; set; }
    public DateTime BirthDay { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string AboutMe { get; set; }
    public byte[] ProfilePicture { get; set; }


    public class Handler:IRequestHandler<CreateUserCommand>
    {
        private readonly ISocialNetworkDbContext _context;
        private readonly IMediator _mediator;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(ISocialNetworkDbContext context, IMediator mediator, IPasswordHasher passwordHasher)
        {
            _context = context;
            _mediator = mediator;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
        await   _context.Users.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new CustomerCreatedNotification { CustomerId = entity.UserId.ToString() }, cancellationToken);
        return Unit.Value;

        }
    }
}