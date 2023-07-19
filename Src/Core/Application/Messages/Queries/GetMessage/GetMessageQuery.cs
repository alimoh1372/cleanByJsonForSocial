using Application.Messages.Queries.GetMessages;
using MediatR;

namespace Application.Messages.Queries.GetMessage;

public record GetMessageQuery(long Id) : IRequest<MessageVm>;
