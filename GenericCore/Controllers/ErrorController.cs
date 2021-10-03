using GenericCore.ViewModels.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace GenericCore.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("Error/{statusCode}")]
        [HttpGet]
        public object HttpStatusCodeHandler(int statusCode)
        {
            APIResponse response = new APIResponse();

            response.ErrorCode = statusCode;

            switch (statusCode)
            {
                case 404:
                    response.ErrorMessage = "The requested url could not be found";
                    break;
                case 401:
                    response.ErrorMessage = "You are not authorized. Please login or register to continue.";
                    break;
                default:
                    response.ErrorCode = 400;
                    response.ErrorMessage = "That didn't work, please have another go.";
                    break;
            }

            return response;
        }
    }
}