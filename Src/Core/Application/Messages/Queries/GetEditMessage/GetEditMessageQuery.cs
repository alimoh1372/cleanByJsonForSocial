using MediatR;

namespace Application.Messages.Queries.GetEditMessage;

public record GetEditMessageQuery(long Id):IRequest<EditMessageVm>;