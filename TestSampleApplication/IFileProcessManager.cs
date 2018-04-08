namespace TestSample
{
    public interface IFileProcessManager
    {
        string FileDirectory { get; set; }
        string OutputFilePath { get; set; }
        void WriteToFiles();
    }

}
