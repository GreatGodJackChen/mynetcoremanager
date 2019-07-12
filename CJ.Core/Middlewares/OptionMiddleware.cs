using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CJ.Core.Middlewares
{
    public class OptionMiddleware
    {
        private readonly RequestDelegate _next;
        public OptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            if (context.Request.Method.ToUpper() == "OPTIONS")
            {
                context.Response.StatusCode = 200;
            }
            else
            {
                await _next(context);
            }
        }
    }
    public static class OptionMiddlewareExtensions
    {
        /// <summary>
        /// 权限检查
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseOptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptionMiddleware>();
        }
    }
}
