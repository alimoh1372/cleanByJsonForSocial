using MediatR;

namespace Application.Messages.Commands.Send;

public record SendCommand(long FkFromUserId, long FkToUserId, string MessageContent):IRequest<Unit>;