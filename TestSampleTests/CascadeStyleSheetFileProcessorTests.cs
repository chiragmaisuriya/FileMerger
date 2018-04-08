using System.Collections.Generic;
using System.IO;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSample.Fakes;

namespace TestSample.Tests
{
    [TestClass()]
    public class CascadeStyleSheetFileProcessorTests
    {
        [TestMethod()]
        public void CascadeStyleSheetFileProcessorTest()
        {
            FileProcessor javaScirptFileProcessor = new JavaScriptFileProcessor();
            Assert.IsNotNull(javaScirptFileProcessor);
        }

        [TestMethod()]
        public void ProcessFileTest()
        {
            //Arrange
            FileProcessor fileProcessor = new CascadeStyleSheetFileProcessor();
            fileProcessor.Files = new List<string>() { "File1.css", "File2.CSS" };
            bool readFileDataGetCalled = false;

            //Act
            using (ShimsContext.Create())
            {
                
                ShimFileProcessor.AllInstances.ReadFileDataString = (a, b) =>
                {
                    readFileDataGetCalled = true;
                    return "FileContent1"+ System.Environment.NewLine + "FileContent2";
                };
                
                fileProcessor.ProcessFile(Directory.GetCurrentDirectory());
            }

            //Assert
            Assert.IsTrue(readFileDataGetCalled);
            Assert.IsTrue(File.Exists(Directory.GetCurrentDirectory() + "\\AllCSS.css"));

        }
        [TestMethod()]
        public void ProcessFileFileHavingImportTest()
        {
            //Arrange
            FileProcessor fileProcessor = new CascadeStyleSheetFileProcessor();
            fileProcessor.Files = new List<string>() { "File1.css", "File2.css" };
            bool readFileDataGetCalled = false;

            //Act
            using (ShimsContext.Create())
            {

                ShimFileProcessor.AllInstances.ReadFileDataString = (a, b) =>
                {
                    readFileDataGetCalled = true;
                    return "@import FileContent1" + System.Environment.NewLine + "FileContent2" + System.Environment.NewLine + "@import FileContent3" + System.Environment.NewLine + "FileContent4";
                };

                fileProcessor.ProcessFile(Directory.GetCurrentDirectory());
            }

            //Assert
            Assert.IsTrue(readFileDataGetCalled);
            Assert.IsTrue(File.Exists(Directory.GetCurrentDirectory() + "\\AllCSS.css"));
            Assert.IsTrue(File.ReadAllLines(Directory.GetCurrentDirectory() + "\\AllCSS.css").Length == 4);

        }

        [TestMethod()]
        public void NeedToProcessTest()
        {

            //Arrange
            FileProcessor fileProcessor = new CascadeStyleSheetFileProcessor();
            fileProcessor.Files = new List<string>() { "File1.css", "File2.CsS" };

            //Act
            bool actual = fileProcessor.NeedToProcess();

            //Assert
            Assert.IsTrue(actual);


        }
        [TestCleanup()]
        public void Cleanup()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\AllCSS.css"))
                File.Delete(Directory.GetCurrentDirectory() + "\\AllCSS.css");
        }
    }
}