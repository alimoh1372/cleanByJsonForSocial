using Application.Common.Interfaces;
using Application.Users.Queries.GetUser;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.Search;

public class SearchQueryHandler:IRequestHandler<SearchQuery,UserListVm>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IMapper _mapper;

    public SearchQueryHandler(ISocialNetworkDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserListVm> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var query  = _context.Users
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider);

        if (!string.IsNullOrWhiteSpace(request.Email))
            query = query.Where(x => x.Email.ToString().ToLower().Contains(request.Email.ToLower()));

        var usersVm = new UserListVm
        {
            Users =await query.ToListAsync(cancellationToken)
        };
        return usersVm;
    }
}