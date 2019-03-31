using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using LifeLogger.Models.Context;

namespace LifeLogger.Web.App_Start
{
    public class DBContextConfig
    {
        public static void AddScope(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LifeLoggerDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("LifeLogger"), b => b.MigrationsAssembly("LifeLogger.Web")));
        }
    }
}
