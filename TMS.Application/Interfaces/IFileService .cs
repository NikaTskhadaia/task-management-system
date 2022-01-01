using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.Application.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<string>> UploadFileAsync(List<IFormFile> files, string subDirectory, CancellationToken cancellationToken);
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory, CancellationToken cancellationToken);
    }
}
