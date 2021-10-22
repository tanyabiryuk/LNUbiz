﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LNUbiz.BLL
{
    public interface IFileStreamManager
    {
        Stream GetStream();

        Task CopyToAsync(MemoryStream memory);

        Task CopyToAsync(IFormFile from, Stream memoryTo);

        Task CopyToAsync(Stream from, Stream memoryTo);

        FileStreamManager GenerateFileStreamManager(string path, FileMode mode);
    }
}