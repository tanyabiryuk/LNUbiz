using System.Net.WebSockets;

namespace LNUbiz.BLL.DTO.Notification
{
    public class ConnectionDTO
    {
        public string ConnectionId { get; set; }
        public WebSocket WebSocket { get; set; }
    }
}
