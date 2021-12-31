using System;
using System.Collections.Generic;
using System.Text.Json;

namespace TMS.Domain.ValueObjects
{
    public class TaskFiles
    {
        public TaskFiles()
        {

        }

        public TaskFiles(IDictionary<Guid, string> filePaths)
        {
            FilePaths = SerializeFilePaths(filePaths);
        }

        public string FilePaths { get; private set; }

        private string SerializeFilePaths(IDictionary<Guid, string> filePaths)
        {
            Dictionary<Guid, string> existingFiles = new();

            if (!string.IsNullOrEmpty(FilePaths))
            {
                existingFiles = JsonSerializer.Deserialize<Dictionary<Guid, string>>(FilePaths);
            }

            foreach (var item in filePaths)
            {
                existingFiles[item.Key] = item.Value;
            }

            var result = JsonSerializer.Serialize(existingFiles);
            return result;
        }
    }
}
