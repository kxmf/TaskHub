using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes
{
    public class ValidateUserRequestAttribute : ActionFilterAttribute
    {
        private const string NameProperty = "Name";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestObject = context.ActionArguments.Values.FirstOrDefault();

            if (requestObject is null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }

            var namePropertyInfo = requestObject.GetType().GetProperty(NameProperty, BindingFlags.Public | BindingFlags.Instance);

            if (namePropertyInfo != null && namePropertyInfo.CanRead)
            {
                var nameValue = namePropertyInfo.GetValue(requestObject) as string;

                if (string.IsNullOrWhiteSpace(nameValue))
                {
                    context.Result = new BadRequestObjectResult("Имя пользователя не задано");
                    return;
                }
            }
            
            base.OnActionExecuting(context);
        }
    }
}