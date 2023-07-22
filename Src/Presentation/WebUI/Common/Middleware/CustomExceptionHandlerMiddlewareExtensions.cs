namespace WebUI.Common.Middleware;

public static class CustomExceptionHandlerMiddlewareExtensions{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder appBuilder)
    {
        return appBuilder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}