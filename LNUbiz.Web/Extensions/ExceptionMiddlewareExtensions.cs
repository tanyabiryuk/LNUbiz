using LNUbiz.Web.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;

namespace LNUbiz.Web.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}