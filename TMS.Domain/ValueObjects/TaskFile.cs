using System.Collections.Generic;
using System.Text;

namespace TMS.Domain.ValueObjects
{
    public class TaskFiles
    {
        public TaskFiles()
        {

        }

        public TaskFiles(IEnumerable<string> filePaths)
        {
            FilePaths = SerializeFilePaths(filePaths);
        }

        public string FilePaths { get; private set; }

        private static string SerializeFilePaths(IEnumerable<string> filePaths)
        {
            StringBuilder sb = new();

            foreach (var item in filePaths)
            {
                sb.Append($"{item};");
            }

            return sb.ToString();
        }
    }
}
