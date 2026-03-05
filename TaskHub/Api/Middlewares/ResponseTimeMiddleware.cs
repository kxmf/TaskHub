using System.Diagnostics;

namespace Api.Middlewares;

public class ResponseTimeMiddleware
{
    private readonly RequestDelegate _next;
 
    public ResponseTimeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();

            context.Response.Headers.Append("X-Response-time-Ms", stopwatch.ElapsedMilliseconds.ToString());

            return Task.CompletedTask;
        });

        await _next(context);
    }
}