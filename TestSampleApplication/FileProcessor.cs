using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestSample
{
    public abstract class FileProcessor
    {
        public abstract void ProcessFile(string outPutFilePath);
        public abstract bool NeedToProcess();
        public List<string> Files { get; set; }
        protected string ReadFileData(string file)
        {
            StringBuilder all = new StringBuilder(string.Empty);
            using (TextReader textReader = new StreamReader(file))
            {
                string line;
                while ((line = textReader.ReadLine()) != null)
                {
                    all.AppendLine(line);
                }
            }

            return all.ToString();
        }

        protected bool SetFilesTobeProcessed(string extension)
        {
            Files = Files.Where(f => string.Compare(Path.GetExtension(f), extension, StringComparison.InvariantCultureIgnoreCase) == 0).ToList();
            return Files.Count > 0;

        }
        protected void WriteToOutputFile(string outputResultFilePath, string all)
        {
            if (File.Exists(outputResultFilePath))
            {
                File.Delete(outputResultFilePath);
            }
            File.WriteAllText(outputResultFilePath, all);
        }
      
    }
}
