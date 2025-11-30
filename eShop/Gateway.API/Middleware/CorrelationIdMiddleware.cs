using Serilog.Context;

namespace Gateway.API.Middleware;

public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeader = "x-correlation-id";
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;

    public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
        {
            correlationId = Guid.NewGuid().ToString();
            context.Request.Headers[CorrelationIdHeader] = correlationId;
        }
        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CorrelationIdHeader] = correlationId;
            return Task.CompletedTask;
        });

        //Log for visibility
        var correlationIdValue = correlationId.ToString();
        if (_logger.IsEnabled(LogLevel.Information))
        {
            using (LogContext.PushProperty("CorrelationId", correlationIdValue))
            {
                _logger.LogInformation("Correlation Id set: {CorrelationId}", correlationIdValue);
                await _next(context);
            }
        }
        else
        {
            await _next(context);
        }
    }
}
