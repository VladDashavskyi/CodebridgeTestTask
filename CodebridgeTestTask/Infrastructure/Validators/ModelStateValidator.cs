using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CodebridgeTestTask.Model;

namespace CodebridgeTestTask.Infrastructure.Validators
{
    public class ModelStateValidator
    {
        /// <summary>
        ///     Called before the action method is invoked
        /// </summary>
        /// <param name="context"></param>
        public static IActionResult ValidateModelState(ActionContext context)
        {
            IEnumerable<ModelError> errorCollection = context.ModelState.Values
                                                             .Where(x => x.Errors.Count > 0 && x.ValidationState == ModelValidationState.Invalid)
                                                             .SelectMany(x => x.Errors);

            Error error = new Error
            {
                Code = 400
                            ,
                Message = string.Join(" ", errorCollection.Select(i => i.ErrorMessage).ToList())
            };
            return new BadRequestObjectResult(error);
        }
    }
}

