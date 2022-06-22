using System.Net;

namespace Web.Services.Models
{
    public class ActionResult
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Object? Object { get; set; }
        public string ErrorMessage { get; set; }
    }
}
