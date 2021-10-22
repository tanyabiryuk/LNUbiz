namespace LNUbiz.BLL.Services.PDF
{
    public class PdfSettings : IPdfSettings
    {
        public PdfSettings()
        {
            Title = "Заява";
            Subject = "Auto generated pdf file";
            Author = "LNUbiz system";
            FontName = "Times New Roman";
            StyleName = "Normal";
            ImagePath = "";
        }

        public string Title { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public string FontName { get; set; }
        public string StyleName { get; set; }
        public string ImagePath { get; set; }
    }
}