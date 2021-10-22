using LNUbiz.Web.CustomMiddlewares;
using LNUbiz.Web.WebSocketHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace LNUbiz.Web.Extensions
{
    public static class WebSocketMiddlewareExtension
    {
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app,
                                                        PathString path,
                                                        BaseWebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketMiddleware>(handler));
        }

    }
}
