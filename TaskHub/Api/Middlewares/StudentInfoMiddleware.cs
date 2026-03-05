namespace Api.Middlewares;

public class StudentInfoMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _name;
    private readonly string _group;

    public StudentInfoMiddleware(RequestDelegate next, string name, string group)
    {
        _next = next;
        _name = name;
        _group = group;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-Student-Name", _name);
        context.Response.Headers.Append("X-Student-Group", _group);
        
        await _next(context);
    }
}