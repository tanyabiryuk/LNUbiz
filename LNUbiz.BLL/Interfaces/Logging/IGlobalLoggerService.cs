using System;

namespace LNUbiz.BLL.Interfaces.Logging
{
    public interface IGlobalLoggerService
    {
        void LogError(Exception ex);
    }
}
