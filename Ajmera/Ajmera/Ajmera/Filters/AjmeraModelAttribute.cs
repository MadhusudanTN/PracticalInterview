using Microsoft.AspNetCore.Mvc.Filters;

namespace Ajmera.Filters
{
    /// <summary>
    /// ValidateModelAttribute this class is used for validating the model class object
    /// </summary>
    public class AjmeraModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}