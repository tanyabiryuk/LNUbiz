using System;
using LNUbiz.BLL.Interfaces;

namespace LNUbiz.BLL.Services
{
    public class DateTimeHelper : IDateTimeHelper
    {
        ///<inheritdoc/>
        DateTime IDateTimeHelper.GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
