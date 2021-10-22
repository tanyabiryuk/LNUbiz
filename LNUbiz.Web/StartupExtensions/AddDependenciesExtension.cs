using LNUbiz.BLL.Interfaces;
using LNUbiz.BLL.Interfaces.Admin;
using LNUbiz.BLL.Interfaces.AzureStorage;
using LNUbiz.BLL.Interfaces.AzureStorage.Base;
using LNUbiz.BLL.Interfaces.Jwt;
using LNUbiz.BLL.Interfaces.Logging;
using LNUbiz.BLL.Interfaces.Notifications;
using LNUbiz.BLL.Interfaces.Resources;
using LNUbiz.BLL.Interfaces.UserProfiles;
using LNUbiz.BLL.SecurityModel;
using LNUbiz.BLL.Services;
using LNUbiz.BLL.Services.Admin;
using LNUbiz.BLL.Services.Auth;
using LNUbiz.BLL.Services.AzureStorage;
using LNUbiz.BLL.Services.AzureStorage.Base;
using LNUbiz.BLL.Services.Jwt;
using LNUbiz.BLL.Services.Logging;
using LNUbiz.BLL.Services.Notifications;
using LNUbiz.BLL.Services.UserProfiles;
using LNUbiz.BLL.Services.EmailSending;
using LNUbiz.BLL.Services.Interfaces;
using LNUbiz.BLL.Services.PDF;
using LNUbiz.BLL.Settings;
using LNUbiz.DAL.Repositories;
using LNUbiz.DAL.Repositories.Realizations.Base;
using LNUbiz.Web.WebSocketHandlers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using LNUbiz.BLL;

namespace LNUbiz.Web.StartupExtensions
{
    public static class AddDependencyExtension
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            services.AddScoped<BusinessTripRequestAccessSettings, BusinessTripRequestAccessSettings>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAdminTypeService, AdminTypeService>();
            services.AddScoped<IAuthEmailService, AuthEmailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDateTimeHelper, DateTimeHelper>();
            services.AddScoped<IEmailContentService, EmailContentService>();
            services.AddScoped<IEmailReminderService, EmailReminderService>();
            services.AddScoped<IEmailSendingService, EmailSendingService>();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IFileStreamManager, FileStreamManager>();
            services.AddScoped<IGlobalLoggerService, GlobalLoggerService>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IResources, BLL.Services.Resources.Resources>();
            services.AddScoped<IUserBlobStorageRepository, UserBlobStorageRepository>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<BusinessTripRequestAccessSettings>();
            services.AddScoped<ISecurityModel, SecurityModel>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IAzureBlobConnectionFactory, AzureBlobConnectionFactory>();
            services.AddSingleton<INotificationConnectionManager, NotificationConnectionManager>();
            services.AddSingleton<UserNotificationHandler>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IUniqueIdService, UniqueIdService>();
            services.AddSingleton<IUserMapService, UserMapService>();
            return services;
        }
    }
}
