using LNUbiz.BLL.Interfaces.AzureStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using LNUbiz.BLL.Interfaces.Logging;
using LNUbiz.DAL.Repositories;
using PdfSharpCore.Pdf;

namespace LNUbiz.BLL.Services.PDF
{
    public class PdfService : IPdfService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILoggerService<PdfService> _logger;

        public PdfService(IRepositoryWrapper repoWrapper, ILoggerService<PdfService> logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        public async Task<byte[]> BusinessTripRequestCreatePDFAsync(int requestId)
        {
            try
            {
                var request = await _repoWrapper.BusinessTripRequests.GetFirstOrDefaultAsync(
                    predicate: r => r.Id == requestId,
                    include: source => source
                        .Include(r => r.User));
                if (request != null)
                {
                    //var base64 = await _decisionBlobStorage.GetBlobBase64Async("dafaultPhotoForPdf.jpg");
                    IPdfSettings pdfSettings = new PdfSettings
                    {
                        Title = $"{request.City} {request.Date}",
                        //ImagePath = base64,
                    };
                    //IPdfCreator creator = new PdfCreator(new PdfDocument());
                    //:to implement
                    return await Task.Run(() => new byte[]{});
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception: {e.Message}");
            }

            return null;
        }
    }
}
