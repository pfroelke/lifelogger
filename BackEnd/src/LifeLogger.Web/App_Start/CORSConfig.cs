using Microsoft.Extensions.DependencyInjection;

namespace LifeLogger.Web.App_Start
{
    public class CORSConfig
    {
        public static void AddScope(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS",
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
        }
    }
}
