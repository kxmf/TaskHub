using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public class StudentInfoHeadersAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        context.HttpContext.Response.Headers.Append("X-Student-Name", "Mannapov Kamil Aidarovich");
        context.HttpContext.Response.Headers.Append("X-Student-Group", "RI-240946");
    }
}