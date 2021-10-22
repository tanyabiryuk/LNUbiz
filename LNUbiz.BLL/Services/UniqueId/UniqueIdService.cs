using System;
using LNUbiz.BLL.Interfaces;

namespace LNUbiz.BLL.Services
{
    public class UniqueIdService : IUniqueIdService
    {
        public Guid GetUniqueId()
        {
            return Guid.NewGuid();
        }
    }
}
