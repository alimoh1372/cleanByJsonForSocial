using Application.Common.Interfaces;
using MediatR;

namespace Application.System.Command;

public class SeedSampleDataCommandHandler:IRequestHandler<SeedSampleDataCommand,Unit>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public SeedSampleDataCommandHandler(ISocialNetworkDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }


    public async Task<Unit> Handle(SeedSampleDataCommand request,CancellationToken cancellationToken)
    {
        var seeder = new SampleSeeder(_context, _passwordHasher);

        await seeder.SeedAllAsync(cancellationToken);
        return Unit.Value;
    }

}