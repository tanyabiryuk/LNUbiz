using System.Text;

namespace LNUbiz.BLL.Services.PDF
{
    public static class PdfHelper
    {
        public static string EncodingHack(string str)
        {
            var encoding = Encoding.BigEndianUnicode;
            var bytes = encoding.GetBytes(str);
            var sb = new StringBuilder();
            sb.Append((char)254);
            sb.Append((char)255);
            for (int i = 0; i < bytes.Length; ++i)
            {
                sb.Append((char)bytes[i]);
            }
            return sb.ToString();
        }
    }
}
