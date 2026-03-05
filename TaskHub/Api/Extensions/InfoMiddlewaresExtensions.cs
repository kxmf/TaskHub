using Api.Middlewares;

namespace Api.Extensions;

public static class InfoMiddlewaresExtensions
{
    public static IApplicationBuilder UseResponseTimeHeader(this IApplicationBuilder app)
    {
        app.UseMiddleware<ResponseTimeMiddleware>();

        return app;
    }

    public static IApplicationBuilder UseStudentHeaders(this IApplicationBuilder app, string name, string group)
    {
        app.UseMiddleware<StudentInfoMiddleware>(name, group);

        return app;
    }
}