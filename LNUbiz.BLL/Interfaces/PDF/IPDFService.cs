using System.Threading.Tasks;

namespace LNUbiz.BLL

{
    public interface IPdfService
    {
        Task<byte[]> BusinessTripRequestCreatePDFAsync(int requestId);
    }
}
