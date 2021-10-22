using LNUbiz.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LNUbiz.Web.StartupExtensions
{
    public static class AddDataAccessExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<LNUbizDBContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("LNUbizDBConnection")));

            return services;
        }
    }
}
