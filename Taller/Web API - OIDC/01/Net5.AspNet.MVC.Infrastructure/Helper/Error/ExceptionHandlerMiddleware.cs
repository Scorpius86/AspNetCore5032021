using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Helper.Error
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                HandleExceptionMessage(context, ex);
            }
        }
        private static void HandleExceptionMessage(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = 500;
            //context.Response.ContentType = "text/html";

            //await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
            //await context.Response.WriteAsync("ERROR!<br><br>\r\n");

            //var exceptionHandlerPathFeature =
            //    context.Features.Get<IExceptionHandlerPathFeature>();

            //if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            //{
            //    await context.Response.WriteAsync(
            //                              "File error thrown!<br><br>\r\n");
            //}

            //await context.Response.WriteAsync(
            //                              "<a href=\"/\">Home</a><br>\r\n");
            //await context.Response.WriteAsync("</body></html>\r\n");
            //await context.Response.WriteAsync(new string(' ', 512));

            context.Response.Redirect("/Blog/Errorpage");

            //context.Response.ContentType = "application/json";
            //int statusCode = (int)HttpStatusCode.InternalServerError;
            //var result = JsonConvert.SerializeObject(new
            //{
            //    StatusCode = statusCode,
            //    ErrorMessage = exception.Message
            //});
            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = statusCode;
            //return context.Response.WriteAsync(result);
        }
    }
}
