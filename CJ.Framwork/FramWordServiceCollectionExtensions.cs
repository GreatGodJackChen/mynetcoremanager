using CJ.Framwork.Ftw.jwt;
using CJ.Framwork.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJ.Framwork
{
    public static class FramWordServiceCollectionExtensions
    {
        public static void AddFramork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<OptionMiddleware>();
            services.AddTransient<AuthActionFilter>();
            services.AddTransient<IJwt, Jwt>();//Jwt注入
        }
          
    }
}
