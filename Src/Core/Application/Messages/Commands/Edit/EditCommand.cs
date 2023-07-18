using MediatR;

namespace Application.Messages.Commands.Edit;

public record EditCommand(long Id, string MessageContent):IRequest<Unit>;