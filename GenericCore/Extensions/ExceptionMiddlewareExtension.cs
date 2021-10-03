using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GenericCore.ViewModels;
using GenericCore.ViewModels.Wrappers;

namespace GenericCore.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null) 
                    {
                        await context.Response.WriteAsync(new APIResponse
                        {
                            ErrorCode = context.Response.StatusCode,
                            Status = true,
                            Errors = new List<string> { contextFeature.Error.Message },
                        }.ToString()); ;
                    }
                });
            });
        }
    }
}
