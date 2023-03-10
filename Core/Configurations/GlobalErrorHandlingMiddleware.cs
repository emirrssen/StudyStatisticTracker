using Core.Exceptions;
using Core.Exceptions.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Configurations
{
    public class GlobalErrorHandlingMiddleware
    {
        RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ExceptionBase ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ExceptionBase ex)
        {
            HttpStatusCode status;
            var stackTrace = String.Empty;
            string message = "";

            if (ex.StatusCode == null)
            {
                status = HttpStatusCode.InternalServerError;
                stackTrace = ex.StackTrace;
                message = ex.Message;
            }
            else
            {
                status = ex.StatusCode;
                stackTrace = ex.StackTrace;
                message = ex.Message;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message });
            // stackTrace loglama için kullanılabilir.
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
