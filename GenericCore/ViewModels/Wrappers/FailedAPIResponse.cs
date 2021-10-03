using GenericCore.Constants;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace GenericCore.ViewModels.Wrappers
{
    public class FailedAPIResponse : APIResponse
    {
        public FailedAPIResponse(ErrorCodes errorCode)
        {
            Status = false;
            ErrorCode = (int)(int)errorCode;
            ErrorMessage = errorCode.ToMessage();
            Errors = new List<string>();
        }

        public FailedAPIResponse(ErrorCodes errorCode, List<string> errors)
        {
            Status = false;
            ErrorCode = (int)errorCode;
            ErrorMessage = errorCode.ToMessage();
            Errors = errors;
        }

        public FailedAPIResponse(ErrorCodes errorCode, IdentityResult identityResult)
        {
            Status = false;
            ErrorCode = (int)errorCode;
            ErrorMessage = errorCode.ToMessage();
            Errors = identityResult.Errors.Select(e => e.Description);
        }
    }
}
