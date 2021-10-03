using GenericCore.Constants;
using System;
using System.Collections.Generic;
using System.Net;

namespace GenericCore.Models.Exceptions
{
    public class BusinessException : Exception
    {
        public ErrorCodes Code { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }

        public readonly List<string> Errors = new List<string>();

        public BusinessException(ErrorCodes errorCode) : base(errorCode.ToMessage())
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
            Code = errorCode;
        }

        public BusinessException(ErrorCodes errorCode, HttpStatusCode httpStatusCode) : base(errorCode.ToMessage())
        {
            Code = errorCode;
            HttpStatusCode = httpStatusCode;
        }

        public BusinessException(ErrorCodes errorCode, string message) : base(message)
        {
            HttpStatusCode = HttpStatusCode.BadRequest;
            Code = errorCode;
        }
    }
}
