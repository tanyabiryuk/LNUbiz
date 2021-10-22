using System;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using LNUbiz.DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace LNUbiz.BLL.Services.PDF.Documents
{
    public class BusinessTripRequestDocument : PdfDocument
    {
        private readonly BusinessTripRequest request;

        public BusinessTripRequestDocument(BusinessTripRequest request, IPdfSettings settings) : base(settings)
        {
            this.request = request;
        }

        public override void SetDocumentBody(PdfPage page, XGraphics gfx)
        {
            SetText(gfx, "Для осіб, які наміряються податись у відрядження", XFontStyle.Regular, 180, 20);
            SetDashLine(gfx, 40, 40, 560, 40);
            SetText(gfx, "форма №1", XFontStyle.Regular, 50, 50);
            SetText(gfx, "Декану факультету Прикладної математики та інформатики", XFontStyle.Regular, 380, 55);
            SetText(gfx, $"від {request.FullName}", XFontStyle.Underline, 380, 65);
            SetText(gfx, "Заява", XFontStyle.Bold, 280, 90);
            SetText(gfx, "Прошу прийняти мене в ",
                XFontStyle.Regular, 70, 110);
            SetText(gfx, "Відповідно до Закону України „Про захист персональних даних“ надаю згоду на обробку та ",
                XFontStyle.Regular, 60, 140);
            SetText(gfx, "з метою забезпечення реалізації відносин в сфері громадської діяльності.",
                XFontStyle.Regular, 50, 170);
            SetText(gfx, "Також посвідчую, що повідомлення про включення даних про мене до бази персональних даних ",
                XFontStyle.Regular, 60, 180);
            SetText(gfx, "отримав/ла, із правами, які я маю відповідно до змісту ст. 8 Закону України „Про захист персональних даних“,",
                XFontStyle.Regular, 50, 190);
            SetText(gfx, "ознайомлений/на.", XFontStyle.Regular, 50, 200);

            SetText(gfx, $"{DateTime.Now:dd.MM.yyyy}, {request.City}", XFontStyle.Underline, 80, 310);
            SetLine(gfx, 370, 310, 460, 310);
            SetText(gfx, $"({request.User.LastName} {request.User.FirstName?[0]}. {(request.User.FatherName.IsNullOrEmpty()? "" : request.User.FatherName+".")})", XFontStyle.Italic, 463, 300);
            SetText(gfx, "Підпис Заявника", XFontStyle.Italic, 410, 310);

            SetDashLine(gfx, 40, 330, 560, 330);

            SetText(gfx, "Анкета заявника", XFontStyle.Bold, 60, 340);
            SetText(gfx, "(точно заповнює заявник)", XFontStyle.Italic, 133, 340);
            SetText(gfx, "Прізвище", XFontStyle.Regular, 50, 360);
            SetLine(gfx, 110, 370, 290, 370);
            SetText(gfx, $"{request.User.LastName}", XFontStyle.Italic, 160, 358);
            SetText(gfx, "Ім’я", XFontStyle.Regular, 320, 360);
            SetLine(gfx, 360, 370, 550, 370);
            SetText(gfx, $"{request.User.FirstName}", XFontStyle.Italic, 420, 358);
            SetText(gfx, "По-батькові", XFontStyle.Regular, 50, 375);
            SetLine(gfx, 110, 385, 290, 385);
            SetText(gfx, $"{request.User.FatherName}", XFontStyle.Italic, 160, 373);
            SetText(gfx, "тел", XFontStyle.Regular, 50, 405);
            SetLine(gfx, 110, 415, 290, 415);
            SetText(gfx, $"{request.User.PhoneNumber}", XFontStyle.Italic, 160, 403);
            SetText(gfx, "Ел. пошта", XFontStyle.Regular, 300, 405);
            SetLine(gfx, 360, 415, 550, 415);
            SetText(gfx, $"{request?.User?.Email}", XFontStyle.Italic, 390, 403);
            SetText(gfx, "Місце праці", XFontStyle.Regular, 50, 435);
            SetLine(gfx, 130, 445, 550, 445);
            SetText(gfx, $"{request.FullTimePosition}", XFontStyle.Italic, 150, 433);
            

            SetDashLine(gfx, 40, 530, 560, 530);
        }

        private static void SetText(XGraphics gfx, string text, XFontStyle style, double x, double y)
        {
            const string facename = "Calibri";

            XStringFormat format = new XStringFormat();
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode);
            XFont font = new XFont(facename, 8, style, options);

            gfx.DrawString(text, font, XBrushes.Black, x, y, format);
        }

        private static void SetDashLine(XGraphics gfx, double x1, double y1, double x2, double y2)
        {
            XPen pen = new XPen(XColors.Black, 0.5);
            pen.DashStyle = XDashStyle.Dash;
            gfx.DrawLine(pen, x1, y1, x2, y2);
        }
        private static void SetLine(XGraphics gfx, double x1, double y1, double x2, double y2)
        {
            XPen pen = new XPen(XColors.Black);
            gfx.DrawLine(pen, x1, y1, x2, y2);
        }

    }
}