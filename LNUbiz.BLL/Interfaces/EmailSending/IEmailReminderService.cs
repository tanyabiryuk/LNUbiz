using System.Threading.Tasks;

namespace LNUbiz.BLL.Interfaces
{
    public interface IEmailReminderService
    {
        Task RemindAdminsToApproveRequests();
    }
}
