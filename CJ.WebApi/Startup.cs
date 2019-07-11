using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CJ.Application;
using CJ.Core;
using CJ.Core.Exception;
using CJ.Core.Ftw.jwt;
using CJ.Data;
using CJ.Domain;
using CJ.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CJ.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(o => {
                o.Filters.Add<GlobalExceptionFilter>();
                o.Filters.Add<AuthActionFilter>();
            } ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCore(Configuration);
            services.AddUow();
            services.AddBaseReposity(Configuration);
            services.AddAllDbContext(Configuration);
            //Autofac容器
            var containerBuilder = new ContainerBuilder();
            //注册依赖
            services.AddUowInterceptor(containerBuilder);
            services.AddAppService(containerBuilder);
            containerBuilder.Populate(services);
            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(containerBuilder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           // app.UseJwt();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
