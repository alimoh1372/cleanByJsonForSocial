using System.Diagnostics;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behavior;

public class RequestPerformanceBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse>
    where TRequest:IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    public RequestPerformanceBehavior(ILogger logger, ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next();
        _timer.Stop();


        if (_timer.ElapsedMilliseconds >5000)
        {
            var name = typeof(TRequest).Name;
            _logger.LogInformation("cleanByJsonForSocial Long Running Request:" +
                                           "{Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}"
                        , name, _timer.ElapsedMilliseconds, _currentUserService.UserId, request);
        }

        return response;
    }
}