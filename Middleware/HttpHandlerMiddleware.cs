using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BasicApplication.Middleware
{

    public class HttpHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var reader = new StreamReader(context.Request.Body);
            var rawMessage = await reader.ReadToEndAsync();

            // Do something with context near the beginning of request processing.
            await _next.Invoke(context);

            // Clean up.
        }
    }

    public static class HttpHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpHandlerMiddleware>();
        }
    }
}
