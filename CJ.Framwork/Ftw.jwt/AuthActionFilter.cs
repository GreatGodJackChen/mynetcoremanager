﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CJ.Framwork.Ftw.jwt
{
    public class AuthActionFilter : IAsyncActionFilter
    {
        private JwtConfig _jwtConfig = new JwtConfig();
        private IJwt _jwt;
        public AuthActionFilter(IConfiguration configration, IJwt jwt)
        {
            this._jwt = jwt;
            configration.GetSection("Jwt").Bind(_jwtConfig);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_jwtConfig.IgnoreUrls.Contains(context.HttpContext.Request.Path))
            {
                await next();
            }
            else
            {
                if (context.HttpContext.Request.Headers.TryGetValue(this._jwtConfig.HeadField, out Microsoft.Extensions.Primitives.StringValues authValue))
                {
                    var authstr = authValue.ToString().Substring(this._jwtConfig.Prefix.Length, authValue.ToString().Length - this._jwtConfig.Prefix.Length);
                    if (this._jwt.ValidateToken(authstr, out Dictionary<string, string> Clims))
                    {
                        //判断是否有权限操作
                        foreach (var item in Clims)
                        {
                            context.HttpContext.Items.Add(item.Key, item.Value);
                        }
                        await next();
                    }
                    else
                    {
                        context.HttpContext.Response.StatusCode = 401;
                        context.HttpContext.Response.ContentType = "application/json";
                        await context.HttpContext.Response.WriteAsync("{\"Code\":401,\"Message\":\"无权访问\"}");
                    }
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync("{\"Code\":401,\"Message\":\"无权访问\"}");
                }
            }

        }
    }
}
