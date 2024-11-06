using CodebridgeTestTask.Bll.Extension;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CodebridgeTestTask.Infrastructure.Attributes
{
    public class ValidatePagingAttribute : ActionFilterAttribute
    {
        /// <summary>
        ///     Called before the action method is invoked
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey("pageSize"))
            {
                bool valueAvailable = context.ActionArguments.TryGetValue("pageSize", out object value);

                if (valueAvailable)
                {
                    if ((value as int? ?? -1) <= 0)
                        throw new ValidationException("Invalid Page Size"); 
                }
                else
                {
                    throw new ValidationException("Invalid Page Size");
                }
            }

            if (context.ActionArguments.ContainsKey("pageNumber"))
            {
                bool valueAvailable = context.ActionArguments.TryGetValue("pageNumber", out object value);

                if (valueAvailable)
                {
                    if ((value as int? ?? -1) <= 0)
                        throw new ValidationException("Invalid Page Number");
                }
                else
                {
                    throw new ValidationException("Invalid Page Number");
                }
            }
        }
    }
}
