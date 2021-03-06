﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using LifeLogger.Web.App_Start;
using LifeLogger.Services;
using Newtonsoft.Json;

namespace LifeLogger.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IJWTHandler, JWTHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkdayService, WorkdayService>();

            var sp = services.BuildServiceProvider();
            var jwthandler = sp.GetService<IJWTHandler>();
            jwthandler.AddAuthentification(services, Configuration);

            CORSConfig.AddScope(services);
            DBContextConfig.AddScope(services, Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); ;

            //services.AddDbContext<ExampleDbContext>(options =>
            //    options.UseInMemoryDatabase("ExampleInMemoryDB"));
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
