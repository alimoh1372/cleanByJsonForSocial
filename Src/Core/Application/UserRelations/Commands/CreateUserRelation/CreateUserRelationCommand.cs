using MediatR;

namespace Application.UserRelations.Commands.CreateUserRelation;

public record CreateUserRelationCommand(long FkUserAId, long FkUserBId, string RelationRequestMessage) :IRequest<Unit>;