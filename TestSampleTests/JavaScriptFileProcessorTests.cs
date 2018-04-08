using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Microsoft.QualityTools.Testing.Fakes;
using TestSample.Fakes;

namespace TestSample.Tests
{
    [TestClass()]
    public class JavaScriptFileProcessorTests
    {
        [TestMethod()]
        public void JavaScriptFileProcessorTest()
        {
           FileProcessor javaScirptFileProcessor = new JavaScriptFileProcessor();
           Assert.IsNotNull(javaScirptFileProcessor);
        }

        [TestMethod()]
        public void ProcessFileTest()
        {
            //Arrange
            FileProcessor fileProcessor = new JavaScriptFileProcessor();
            fileProcessor.Files = new List<string>() { "File1.js", "File2.js" };
            bool readFileDataGetCalled = false;

            //Act
            using (ShimsContext.Create())
            {

                ShimFileProcessor.AllInstances.ReadFileDataString = (a, b) =>
                {
                    readFileDataGetCalled = true;
                    return "FileContent1" + System.Environment.NewLine + "FileContent2";
                };

                fileProcessor.ProcessFile(Directory.GetCurrentDirectory());
            }

            //Assert
            Assert.IsTrue(readFileDataGetCalled);
            Assert.IsTrue(File.Exists(Directory.GetCurrentDirectory() + "\\AllJs.js"));
        }

        [TestMethod()]
        public void NeedToProcessTest()
        {

            //Arrange
            FileProcessor fileProcessor = new JavaScriptFileProcessor();
            fileProcessor.Files = new List<string>() { "File1.js", "File2.JS" };

            //Act
            bool actual = fileProcessor.NeedToProcess();

            //Assert
            Assert.IsTrue(actual);


        }
        [TestCleanup()]
        public void Cleanup()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "\\AllJs.js"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\AllJs.js");
            }
        }
    }
}