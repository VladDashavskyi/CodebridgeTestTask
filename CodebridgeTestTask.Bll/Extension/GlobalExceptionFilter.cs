using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace CodebridgeTestTask.Bll.Extension
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {

                ValidationException => StatusCodes.Status400BadRequest,

                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

                _ => StatusCodes.Status500InternalServerError
            };

            context.Result = new ObjectResult(new
            {
                code = statusCode,
                message = context.Exception.Message
            });
        }
    }
}
