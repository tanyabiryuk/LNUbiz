using System.Collections.Concurrent;
using System.Collections.Generic;
using LNUbiz.BLL.DTO.Notification;
using LNUbiz.BLL.Interfaces.Notifications;

namespace LNUbiz.BLL.Services.Notifications
{
    public class UserMapService : IUserMapService
    {
        private static readonly ConcurrentDictionary<string, HashSet<ConnectionDTO>> userMap = new ConcurrentDictionary<string, HashSet<ConnectionDTO>>();
        public ConcurrentDictionary<string, HashSet<ConnectionDTO>> UserConnections { get { return userMap; } }
    }
}
