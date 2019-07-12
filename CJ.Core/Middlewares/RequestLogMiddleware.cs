using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CJ.Core.Middlewares
{
    public class RequestLogMiddleware 
    {
        private RequestDelegate _next;
        public RequestLogMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine($"request path: {context.Request.Path}\r\n");
            await context.Response.WriteAsync($"request path: {context.Request.Path}\r\n");

            await _next.Invoke(context);

            Console.WriteLine("finish request!");
            await context.Response.WriteAsync("finish request!\r\n");
        }
    }
}
