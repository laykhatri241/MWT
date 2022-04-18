using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MWTWebApi.Model
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
                error =>
                {
                    error.Run(async context =>
                   {
                       context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                       context.Response.ContentType = "application/json";

                       var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();
                       if (contextFeatures != null)
                       {
                           await context.Response.WriteAsync(
                               JsonConvert.SerializeObject(
                                   new HttpAPIResponse()
                                   {
                                       ErrorMessage = "Server Internal Error",
                                       Timestamp = DateTime.Now,
                                       StatusCode = HttpStatusCode.InternalServerError
                                   }
                                   ));
                       }
                   });
                }
                );
        }
    }
}
