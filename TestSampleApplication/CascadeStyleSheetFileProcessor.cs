using System;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TestSample
{
    public class CascadeStyleSheetFileProcessor : FileProcessor
    {
        private const string Import = "@import";
        private const string Extension = ".css";
        private const string Cssoutputfilename = "CSSOutputFileName";


        public override void ProcessFile(string outPutFilePath)
        {
            StringBuilder all = new StringBuilder();
            foreach (var file in Files)
            {
                string fileContent = ReadFileData(file);

                string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                //Filter out Lines with @import 
                var outputLines = string.Join(Environment.NewLine,lines.Where(line => !line.Contains(Import)));
                if (fileContent.Contains(Import))
                {
                    Console.WriteLine(Resource
                        .CascadeStyleSheetFileProcessor_ProcessFile_Import_links_are_not_considered);
                }
                all.AppendLine(outputLines);

            }
            var outputFileName = ConfigurationManager.AppSettings[Cssoutputfilename];
            var outputResultFilePath = outPutFilePath + outputFileName;
            WriteToOutputFile(outputResultFilePath, all.ToString());
        }

        public override bool NeedToProcess()
        {
            return SetFilesTobeProcessed(Extension);
        }

    }
}