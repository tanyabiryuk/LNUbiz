using LNUbiz.Web.StartupExtensions;
using LNUbiz.BLL.Services.Jwt;
using LNUbiz.BLL.Settings;
using LNUbiz.DAL;
using LNUbiz.DAL.Entities;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LNUbiz.Web.StartupExtensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMvc();
            services.AddAutoMapper();
            services.AddHangFire();
            services.AddHangfireServer();
            services.AddAuthentication(Configuration);
            services.AddDataAccess(Configuration);
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<LNUbizDBContext>()
                    .AddDefaultTokenProviders();
            services.AddCors();
            services.AddSwagger();
            
            services.AddControllers()
                    .AddNewtonsoftJson();
            services.AddLogging();
            services.Configure<EmailServiceSettings>(Configuration.GetSection("EmailServiceSettings"));
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            services.AddAuthorization();
            services.AddLocalization();
            services.AddRequestLocalizationOptions();
            services.AddIdentityOptions();

            services.AddDependency();

            return services;
        }
    }
}
