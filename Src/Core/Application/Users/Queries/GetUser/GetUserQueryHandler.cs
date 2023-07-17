using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Users.Queries.GetUser;

public class GetUserQueryHandler:IRequestHandler<GetUserQuery,UserViewModel>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IMapper _mapper;
    public GetUserQueryHandler(ISocialNetworkDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserViewModel> Handle(GetUserQuery request,CancellationToken cancellationToken)
    {
        var entity =await _context.Users.FindAsync(request.Id,cancellationToken);
        if (entity == null)
            throw new NotFoundException(nameof(User), request.Id);

        return _mapper.Map<UserViewModel>(entity);
    }
}