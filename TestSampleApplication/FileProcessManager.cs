using System.IO;
using System.Linq;

namespace TestSample
{
    public class FileProcessManager : IFileProcessManager
    {
        private readonly FileProcessor[] _fileProcessors;
        public string FileDirectory { get; set; }
        public string OutputFilePath { get; set; }

        public FileProcessManager(FileProcessor [] fileProcessors)
        {
            _fileProcessors = fileProcessors;
        }

        public void WriteToFiles()
        {
            string[] files = GetFiles();
            if (files == null || files.Length <= 0) return;
            foreach (var fileProcessor in _fileProcessors)
            {
                fileProcessor.Files = files.ToList();
                if (fileProcessor.NeedToProcess())
                {
                    fileProcessor.ProcessFile(OutputFilePath);
                }
            }
        }

        private string[] GetFiles()
        {
            string[] files = null;
            if (Directory.Exists(FileDirectory))
            {
                files = Directory.GetFiles(FileDirectory);
            }
            return files;
        }
    }
}
