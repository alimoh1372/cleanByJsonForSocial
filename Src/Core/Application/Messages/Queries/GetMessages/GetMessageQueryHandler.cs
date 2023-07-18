using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Queries.GetMessages;

public class GetMessageQueryHandler : IRequestHandler<GetMessagesQuery, MessageListVm>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IAuthHelper _authHelper;
    private readonly IMapper _mapper;

    public GetMessageQueryHandler(ISocialNetworkDbContext context, IAuthHelper authHelper, IMapper mapper)
    {
        _context = context;
        _authHelper = authHelper;
        _mapper = mapper;
    }

    public async Task<MessageListVm> Handle(GetMessagesQuery request,CancellationToken cancellationToken)
    {
        var user = _authHelper.GetUserInfo();
        if (user.Id != request.IdUserACurrentUser)
            throw new NotAuthorizedRequestException();
        return new MessageListVm
        {
            Messages =await _context.Messages
                .Include(x => x.FromUser)
                .Include(x => x.ToUser)
                .ProjectTo<MessageVm>(_mapper.ConfigurationProvider)
                .Where(x => (x.FkFromUserId == request.IdUserACurrentUser && x.FkToUserId == request.IdUserB)
                            || (x.FkFromUserId == request.IdUserB && x.FkToUserId == request.IdUserACurrentUser))
                .ToListAsync(cancellationToken)
        };
    }

}