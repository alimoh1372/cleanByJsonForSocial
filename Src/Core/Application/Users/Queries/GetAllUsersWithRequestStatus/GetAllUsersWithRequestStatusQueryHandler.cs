using System.Linq;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Utility;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetAllUsersWithRequestStatus;

public class GetAllUsersWithRequestStatusQueryHandler:IRequestHandler<GetAllUsersWithRequestStatusQuery,UserWithRequestStatusListVm>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IAuthHelper _authHelper;
    private readonly IMapper _mapper;
    private  IUserAndUserRelationService _userAndRelationService;

    public GetAllUsersWithRequestStatusQueryHandler(ISocialNetworkDbContext context, IMapper mapper, IAuthHelper authHelper, IUserAndUserRelationService userAndRelationService)
    {
        _context = context;
        _mapper = mapper;
        _authHelper = authHelper;
        _userAndRelationService = userAndRelationService;
    }

    public async Task<UserWithRequestStatusListVm> Handle(GetAllUsersWithRequestStatusQuery request,
        CancellationToken cancellationToken)
    {
        
        //Handle User exception
        var authenticatedUser = await _authHelper.GetUserInfo();
        if (authenticatedUser == null)
            throw new NotAuthenticatedRequestException();
        //Get all user expect current user
        var userWithRequestStatusVms = await _context.Users
            .Where(x => x.UserId != authenticatedUser.Id)
            .ProjectTo<UserWithRequestStatusVm>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        var userRelation = await _context.UserRelations.AsNoTracking().ToListAsync(cancellationToken);
        _userAndRelationService = new UserAndUserRelationServices();

        userWithRequestStatusVms =
          await  _userAndRelationService.FillAllOtherUserWithRequestProperties(userWithRequestStatusVms, userRelation, authenticatedUser.Id, cancellationToken);


        return new UserWithRequestStatusListVm
        {
            Users = userWithRequestStatusVms
        };
    }
}