using LNUbiz.Web.Extensions;
using LNUbiz.Web.StartupExtensions;
using LNUbiz.Web.WebSocketHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Microsoft.AspNetCore.Localization;

namespace LNUbiz.Web
{
    public class Startup
    {
        private string[] _secrets = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IRecurringJobManager recurringJobManager,
                              IServiceProvider serviceProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/V1/swagger.json", "MyApi"); });
            var supportedCultures = new[]
            {
                new CultureInfo("uk-UA"),
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("uk")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("uk-UA"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
            else
            {
                app.ConfigureCustomExceptionMiddleware();
                app.UseHsts();
            }
            app.UseWebSockets();
            app.MapWebSocketManager("/notifications", serviceProvider.GetService<UserNotificationHandler>());
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHangfireDashboard();
            app.Run(async (context) =>
            {
                foreach (string secret in _secrets)
                {
                    var result = string.IsNullOrEmpty(secret) ? "Null" : "Not Null";
                    await context.Response.WriteAsync($"Secret is {result}");
                }
            });
            serviceProvider.AddRecurringJobs(recurringJobManager, Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _secrets = new string[]
            {
                Configuration["StorageConnectionString"],
                Configuration["GoogleAuthentication:GoogleClientSecret"],
                Configuration["GoogleAuthentication:GoogleClientId"],
                Configuration["EmailServiceSettings:SMTPServerPassword"],
                Configuration["EmailServiceSettings:SMTPServerLogin"],
                Configuration["Admin:Password"],
                Configuration["Admin:Email"]
            };

            services.AddServices(Configuration);
        }
    }
}
