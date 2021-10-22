using System.Reflection.Metadata;

namespace LNUbiz.BLL
{
    internal interface IPdfDocument
    {
        PdfSharpCore.Pdf.PdfDocument GetDocument();
    }
}