using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
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

        public async Task<IDictionary<Guid, string>> UploadFileAsync(List<IFormFile> files, string subDirectory)
        {
            Dictionary<Guid, string> paths = new();
            string directory = $"TaskFiles/{DateTime.Now.Year}/{DateTime.Now.Month}";
            var target = Path.Combine(_hostingEnvironment.ContentRootPath, directory, subDirectory);

            Directory.CreateDirectory(target);
            foreach (var file in files)
            {
                if (file.Length <= 0) continue;
                var filePath = Path.Combine(target, file.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
                paths.Add(Guid.NewGuid(), filePath);
            }
            return paths;
        }

        public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
        {
            var zipName = $"archive-{DateTime.Now:yyyy_MM_dd-HH_mm_ss}.zip";

            var files = Directory.GetFiles(Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory)).ToList();

            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                files.ForEach(file =>
                {
                    var theFile = archive.CreateEntry(file);
                    using var streamWriter = new StreamWriter(theFile.Open());
                    streamWriter.Write(File.ReadAllText(file));

                });
            }
            return ("application/zip", memoryStream.ToArray(), zipName);
        }
    }
}
