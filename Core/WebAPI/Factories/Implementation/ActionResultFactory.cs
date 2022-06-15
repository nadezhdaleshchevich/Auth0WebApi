using Microsoft.AspNetCore.Mvc;
using WebAPI.Factories.Interfaces;
using ActionResult = Web.Services.Models.ActionResult;

namespace WebAPI.Factories.Implementation
{
    internal class ActionResultFactory : IActionResultFactory
    {
        public IActionResult CreateActionResult(ActionResult? actionResult)
        {
            if (actionResult == null)
            {
                return new BadRequestResult();
            }

            if (!actionResult.IsSuccess && !string.IsNullOrEmpty(actionResult.ErrorMessage))
            {
                return new ObjectResult(new {Message = actionResult.ErrorMessage})
                {
                    StatusCode = (int)actionResult.StatusCode
                };
            }

            if (actionResult.Object != null)
            {
                return new ObjectResult(actionResult.Object)
                {
                    StatusCode = (int)actionResult.StatusCode
                };
            }

            return new StatusCodeResult((int)actionResult.StatusCode);
        }
    }
}
