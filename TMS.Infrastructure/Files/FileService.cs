using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Interfaces;

namespace TMS.Infrastructure.Files
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IEnumerable<string>> UploadFileAsync(List<IFormFile> files, string subDirectory, CancellationToken cancellationToken)
        {
            List<string> paths = new();
            string directory = $"TaskFiles/{DateTime.Now.Year}/{DateTime.Now.Month}";
            var target = Path.Combine(_hostingEnvironment.ContentRootPath, directory, subDirectory);

            Directory.CreateDirectory(target);
            foreach (var file in files)
            {
                if (file.Length <= 0) continue;
                var filePath = Path.Combine(target, file.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);
                paths.Add(filePath);
            }
            
            return Directory.GetFiles(target);
        }

        public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory, CancellationToken cancellationToken)
        {
            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.zip";

            var files = Directory.GetFiles(Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory)).ToList();

            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                files.ForEach(async file =>
                {
                    var theFile = archive.CreateEntry(file);
                    using var streamWriter = new StreamWriter(theFile.Open());
                    var currentFile = await File.ReadAllTextAsync(file, cancellationToken);
                    await streamWriter.WriteAsync(currentFile.ToCharArray(), cancellationToken);

                });
            }
            return ("application/zip", memoryStream.ToArray(), zipName);
        }
    }
}
