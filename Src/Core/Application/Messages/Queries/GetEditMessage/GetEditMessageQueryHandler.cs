using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Messages.Queries.GetEditMessageQuery;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Queries.GetEditMessage;

public class GetEditMessageQueryHandler:IRequestHandler<GetEditMessageQuery,EditMessageVm>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAuthHelper _authHelper;

    public GetEditMessageQueryHandler(ISocialNetworkDbContext context, IMapper mapper, IAuthHelper authHelper)
    {
        _context = context;
        _mapper = mapper;
        _authHelper = authHelper;
    }


    public async Task<EditMessageVm> Handle(GetEditMessageQuery request,CancellationToken cancellationToken)
    {
        var message =await _context.Messages.SingleOrDefaultAsync(x=>x.Id== request.Id
            ,cancellationToken);

        if (message == null)
            throw new NotFoundException(nameof(Message), request.Id);

        var user =await _authHelper.GetUserInfo();

        if (user.Id != message.FkFromUserId)
            throw new NotAuthorizedRequestException();

        return _mapper.Map<EditMessageVm>(message);
    }
}