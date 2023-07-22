using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behavior;

public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ILogger _logger;
    private readonly IAuthHelper _currentUserService;
    public RequestLogger(ILogger logger, IAuthHelper currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("CleanByJson for social network request:{Name} {@UserId} {@Request}", requestName, _currentUserService.GetUserInfo().Result.Id, request);
        return Task.CompletedTask;
    }
}