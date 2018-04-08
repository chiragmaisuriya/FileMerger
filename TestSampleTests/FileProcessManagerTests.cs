using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSample.Fakes;

namespace TestSample.Tests
{
    [TestClass]
    public class FileProcessManagerTests
    {
        [TestMethod]
        public void FileProcessManagerTest()
        {
            //Arrange
            var stubCascadeStyleSheetFileProcessor =
                new StubCascadeStyleSheetFileProcessor();
            var stubJavaScriptFileProcessor = new StubJavaScriptFileProcessor();

            FileProcessor[] fileProcessors = { stubCascadeStyleSheetFileProcessor, stubJavaScriptFileProcessor };
            IFileProcessManager fileProcessManager = new FileProcessManager(fileProcessors);

            Assert.IsNotNull(fileProcessManager);
        }

        [TestMethod]
        public void WriteToFilesTest()
        {
            //Arrange
            var cascadeStyleSheetFileProcessorProcessFileGetCalled = false;
            var javaScriptFileProcessorProcessFileGetCalled = false;

            var stubCascadeStyleSheetFileProcessor = new StubCascadeStyleSheetFileProcessor
            {
                NeedToProcess01 = () => true,
                ProcessFileString = a => { cascadeStyleSheetFileProcessorProcessFileGetCalled = true; }
            };
            var stubJavaScriptFileProcessor = new StubJavaScriptFileProcessor
            {
                NeedToProcess01 = () => true,
                ProcessFileString = a => { javaScriptFileProcessorProcessFileGetCalled = true; }
            };

            FileProcessor[] fileProcessors = { stubCascadeStyleSheetFileProcessor, stubJavaScriptFileProcessor };
            IFileProcessManager fileProcessManager = new FileProcessManager(fileProcessors);
            var files = new List<string> { "File1.js", "File2.css" };

            //Act
            using (ShimsContext.Create())
            {
                ShimFileProcessManager.AllInstances.GetFiles = a => files.ToArray();
                fileProcessManager.WriteToFiles();
            }

            //Assert
            Assert.IsTrue(cascadeStyleSheetFileProcessorProcessFileGetCalled);
            Assert.IsTrue(javaScriptFileProcessorProcessFileGetCalled);
        }
    }
}