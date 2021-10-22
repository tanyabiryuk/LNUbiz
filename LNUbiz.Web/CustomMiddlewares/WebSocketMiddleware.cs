﻿using LNUbiz.BLL.Interfaces.Notifications;
using LNUbiz.Web.WebSocketHandlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace LNUbiz.Web.CustomMiddlewares
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly BaseWebSocketHandler _webSocketHandler;

        public WebSocketMiddleware(RequestDelegate next,
                                            BaseWebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var userId = context.Request.Query["userId"];
            var id = _webSocketHandler.OnConnected(userId, socket);

            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await _webSocketHandler.ReceiveAsync(socket, result, buffer);
                    return;
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocketHandler.OnDisconnectedAsync(userId, id);
                    return;
                }
            });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);
                handleMessage(result, buffer);
            }
        }
    }
}
