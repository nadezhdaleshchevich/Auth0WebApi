using Microsoft.AspNetCore.Mvc;
using ActionResult = Web.Services.Models.ActionResult;

namespace WebAPI.Factories.Interfaces
{
    public interface IActionResultFactory
    {
        IActionResult CreateActionResult(ActionResult actionResult);
    }
}
