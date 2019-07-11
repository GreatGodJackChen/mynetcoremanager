﻿using CJ.Core.Caching;
using CJ.Core.Ftw.jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJ.Core
{
    public static class CoreServiceCollectionExtensions
    {
        public static void AddCore(this IServiceCollection services,IConfiguration configuration)
        {
            var redisConnection = configuration.GetSection("Redis").Value;
            services.AddDistributedRedisCache(option => option.Configuration = redisConnection);
            services.AddTransient<ICacheManager, RedisCacheManager>();

            services.AddTransient<AuthActionFilter>();
            services.AddTransient<IJwt, Jwt>();//Jwt注入
        }
    }
}