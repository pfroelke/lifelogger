using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using LifeLogger.Models.Context;

namespace LifeLogger.Web
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200",
                        "https://localhost:4200",
                        "http://example.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services.AddDbContext<LifeLoggerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LifeLogger"), b => b.MigrationsAssembly("LifeLogger.Web")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


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
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseMvc();
        }
    }
}
