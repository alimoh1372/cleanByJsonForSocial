using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Messages.Commands.Edit;

public class EditCommandHandler : IRequestHandler<EditCommand, Unit>
{
    private readonly ISocialNetworkDbContext _context;
    private readonly IAuthHelper _authHelper;

    public EditCommandHandler(ISocialNetworkDbContext context, IAuthHelper authHelper)
    {
        _context = context;
        _authHelper = authHelper;
    }

    public async Task<Unit> Handle(EditCommand request, CancellationToken cancellationToken)
    {
        var message = await _context.Messages.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (message == null)
            throw new NotFoundException(nameof(Message), request.Id);
        var user = _authHelper.GetUserInfo();
        if (user.Id != message.FkFromUserId)
            throw new NotAuthorizedRequestException();
        if (message.Created.AddMinutes(3) < DateTime.Now)
            throw new TimeOverToEditException();
        message.MessageContent = request.MessageContent;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;

    }
}

public class TimeOverToEditException : Exception
{
    public TimeOverToEditException(string message = ApplicationMessage.EditTimeOver) : base(message)
    {

    }
}