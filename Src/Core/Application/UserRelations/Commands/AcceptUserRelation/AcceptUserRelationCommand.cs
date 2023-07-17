using MediatR;

namespace Application.UserRelations.Commands.AcceptUserRelation;

public record AcceptUserRelationCommand(long UserIdRequestSentFromIt, long UserIdRequestSentToIt) : IRequest<Unit>;
