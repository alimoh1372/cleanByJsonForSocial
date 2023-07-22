using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;
using Domain.Exceptions;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace WebUI.Common.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, CancellationToken cancellationToken)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, cancellationToken);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Failures);
                break;
            case DuplicationException duplicateException:
                code = HttpStatusCode.Conflict;
                result = JsonSerializer.Serialize(duplicateException.Message);
                break;
            case NotAuthenticatedRequestException notAuthenticatedRequestException:
                code = HttpStatusCode.Unauthorized;
                result = notAuthenticatedRequestException.Message;
                break;
            case NotAuthorizedRequestException notAuthorized:
                code = HttpStatusCode.Forbidden;
                result = notAuthorized.Message;
                break;
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                result = notFoundException.Message;
                break;

            //Domain Exceptions

            case EmailNotMatchException emailNotMatch:
                code = HttpStatusCode.BadRequest;
                result = emailNotMatch.Message;
                break;

        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);

    }
}