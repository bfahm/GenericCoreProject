using GenericCore.Constants;
using Microsoft.AspNetCore.Mvc;

namespace GenericCore.Controllers.Views
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        [HttpGet("[controller]/{errorCode}")]
        public IActionResult Index([FromRoute] ErrorCodes errorCode)
        {
            var errorCodeIsZero = (int)errorCode == 0;

            ViewData["ERROR_CODE"] = errorCodeIsZero ? "Unknown" : (int)errorCode;
            ViewData["ERROR_MESSAGE"] = errorCode.ToMessage();
            return View();
        }
    }
}
