using System.IO;

namespace LNUbiz.BLL
{
    public interface IDirectoryManager
    {
        bool Exists(string path);

        DirectoryInfo CreateDirectory(string path);

        void Move(string sourceDirName, string destDirName);

        string[] GetFiles(string path);
    }
}