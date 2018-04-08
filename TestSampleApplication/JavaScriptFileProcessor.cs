using System.Configuration;
using System.Text;

namespace TestSample
{
    public class JavaScriptFileProcessor : FileProcessor
    {
        private const string Extension = ".js";
        private const string Jsoutputfilename = "JSOutputFileName";

        public override void ProcessFile(string outPutFilePath)
        {
            StringBuilder  all = new StringBuilder();
            foreach (var file in Files)
            {
                all.AppendLine(ReadFileData(file));
            }
            var outputFileName = ConfigurationManager.AppSettings[Jsoutputfilename];
            var outputResultFilePath = outPutFilePath + outputFileName;
            WriteToOutputFile(outputResultFilePath, all.ToString());
        }
        public override bool NeedToProcess()
        {
            return SetFilesTobeProcessed(Extension);
        }

       
    }
}
