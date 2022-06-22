using System.Net;
using Web.Services.Models;

namespace Web.Services.Extensions
{
    internal static class ActionResultExtensions
    {
        public static void OkResult(this ActionResult actionResult, Object obj = null)
        {
            actionResult.IsSuccess = true;
            actionResult.StatusCode = HttpStatusCode.OK;
            actionResult.Object = obj;
            actionResult.ErrorMessage = null;
        }

        public static void CreatedResult(this ActionResult actionResult, Object obj = null)
        {
            actionResult.IsSuccess = true;
            actionResult.StatusCode = HttpStatusCode.Created;
            actionResult.Object = obj;
            actionResult.ErrorMessage = null;
        }

        public static void BadRequestResult(this ActionResult actionResult, string errorMessage, Object obj = null)
        {
            actionResult.IsSuccess = false;
            actionResult.StatusCode = HttpStatusCode.BadRequest;
            actionResult.Object = obj;
            actionResult.ErrorMessage = errorMessage;
        }

        public static void NotFoundResult(this ActionResult actionResult, string errorMessage, Object obj = null)
        {
            actionResult.IsSuccess = false;
            actionResult.StatusCode = HttpStatusCode.NotFound;
            actionResult.Object = obj;
            actionResult.ErrorMessage = errorMessage;
        }
    }
}
