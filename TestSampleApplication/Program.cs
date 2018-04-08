using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace TestSample
{
    static class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            var fileProcessManager = container.Resolve<IFileProcessManager>();
           
            try
            {
                if (args != null && args.Length == 2)
                {
                    fileProcessManager.FileDirectory = args[0];
                    fileProcessManager.OutputFilePath = args[1];
                }
                fileProcessManager.WriteToFiles();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private static void RegisterTypes(UnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
                
            }
            container.RegisterType<IFileProcessManager, FileProcessManager>();
            container.RegisterType<FileProcessor, CascadeStyleSheetFileProcessor>("CascadeStyleSheetFileProcessor");
            container.RegisterType<FileProcessor, JavaScriptFileProcessor>("JavaScriptFileProcessor");
            container.RegisterType<IEnumerable<FileProcessor>, FileProcessor[]>();
        }
    }

  
}