using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Messages.Queries.GetMessages;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Queries.GetMessage;

public class GetMessageQueryHandler:IRequestHandler<GetMessageQuery,MessageVm>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthHelper _authHelper;

    public GetMessageQueryHandler(ISocialNetworkDbContext context, IMapper mapper, IAuthHelper authHelper)
    {
        _context = context;
        _mapper = mapper;
        _authHelper = authHelper;
    }

    public async Task<MessageVm> Handle(GetMessageQuery request,CancellationToken cancellationToken)
    {
        var message =await _context.Messages.SingleOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
        if (message == null)
            throw new NotFoundException(nameof(Message), request.Id);
        var user =await _authHelper.GetUserInfo();
        if (message.FkFromUserId != user.Id)
            throw new NotAuthorizedRequestException();
        return _mapper.Map<MessageVm>(message);
    }
}