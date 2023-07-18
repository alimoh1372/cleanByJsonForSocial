using MediatR;

namespace Application.Messages.Queries.GetMessages;

public record GetMessagesQuery(long IdUserACurrentUser, long IdUserB):IRequest<MessageListVm>;