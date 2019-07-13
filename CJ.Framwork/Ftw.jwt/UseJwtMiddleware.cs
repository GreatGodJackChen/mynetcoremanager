using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CJ.Framwork.Ftw.jwt
{
    public class UseJwtMiddleware
    {
        private readonly RequestDelegate _next;
        private JwtConfig _jwtConfig = new JwtConfig();
        private IJwt _jwt;
        public UseJwtMiddleware(RequestDelegate next, IConfiguration configration, IJwt jwt)
        {
            _next = next;
            this._jwt = jwt;
            configration.GetSection("Jwt").Bind(_jwtConfig);
        }
        public Task InvokeAsync(HttpContext context)
        {
            if (_jwtConfig.IgnoreUrls.Contains(context.Request.Path))
            {
                return this._next(context);
            }
            else
            {
                if (context.Request.Headers.TryGetValue(this._jwtConfig.HeadField, out Microsoft.Extensions.Primitives.StringValues authValue))
                {
                    var authstr = authValue.ToString().Substring(this._jwtConfig.Prefix.Length, authValue.ToString().Length - this._jwtConfig.Prefix.Length);
                    if (this._jwt.ValidateToken(authstr, out Dictionary<string, string> Clims))
                    {
                        foreach (var item in Clims)
                        {
                            context.Items.Add(item.Key, item.Value);
                        }
                        return this._next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"status\":401,\"statusMsg\":\"auth vaild fail\"}");
                    }
                }
                else
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"status\":401,\"statusMsg\":\"auth vaild fail\"}");
                }
            }
        }
    }
    public static class UseUseJwtMiddlewareExtensions
    {
        /// <summary>
        /// 权限检查
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwt(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UseJwtMiddleware>();
        }
    }
}
