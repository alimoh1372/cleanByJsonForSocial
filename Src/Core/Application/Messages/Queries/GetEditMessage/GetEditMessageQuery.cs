using Application.Messages.Queries.GetEditMessageQuery;
using MediatR;

namespace Application.Messages.Queries.GetEditMessage;

public record GetEditMessageQuery(long Id):IRequest<EditMessageVm>;