using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMS.Application.Interfaces
{
    public interface IFileService
    {
        Task<IDictionary<Guid, string>> UploadFileAsync(List<IFormFile> files, string subDirectory);
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);
    }
}
