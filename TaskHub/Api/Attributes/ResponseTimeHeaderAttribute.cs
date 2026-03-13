using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public class ResponseTimeHeaderAttribute : Attribute, IActionFilter
{
    private Stopwatch _stopwatch;

    public ResponseTimeHeaderAttribute()
    {
        _stopwatch = new Stopwatch();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        context.HttpContext.Response.Headers.Append("X-Response-Time-Ms", _stopwatch.ElapsedMilliseconds.ToString());
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch.Start();
    }
}