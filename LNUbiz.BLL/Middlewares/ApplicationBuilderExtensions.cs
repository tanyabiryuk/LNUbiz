using Microsoft.AspNetCore.Builder;

namespace LNUbiz.BLL.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAntiforgeryTokens(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();
        }
    }
}
