using System.IO;

namespace LNUbiz.BLL
{
    public class FileManager : IFileManager
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}