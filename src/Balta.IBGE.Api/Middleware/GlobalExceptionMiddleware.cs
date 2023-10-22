using System.Net;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

namespace Balta.IBGE.Api.Middleware;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[{@TraceId}] An error occurred in [{@RequestMethod}] {@RequestPath}",
                context.TraceIdentifier,
                context.Request.Method,
                context.Request.Path);

            var problemDetail = new ProblemDetails
            {
                Title = "Server error",
                Type = "Internal server error",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = "An internal error occurred"
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetail));
        }
    }
}
