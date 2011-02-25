using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Renamerinator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ShowUsage();
            }
            else
            {
                var directory = args[0];
                if (Directory.Exists(directory))
                {
                    ProcessDirectory(directory);
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(String.Format("Directory {0} does not exist", directory));
                    ShowUsage();
                }
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine(@"Usage: ");
            Console.WriteLine(@" Renamerinator 'path to folder'");
            Console.WriteLine();
            Console.WriteLine(@" Items in path will be renamed in place - no copies will be made.");
            Console.WriteLine();
        }

        private static void ProcessDirectory(string directory)
        {
            var files = Directory.GetFiles(directory);
            foreach (var file in files)
            {
                var newFile = directory.EndsWith("\\") ? directory : directory + "\\";
                var fileExists = true;
                while (fileExists)
                {
                    var newName = Path.GetRandomFileName();
                    var newExt = Path.GetExtension(newName);
                    var oldExt = Path.GetExtension(file);
                    newFile += newName.Replace(newExt, oldExt);
                    fileExists = File.Exists(newFile);
                }
                Console.WriteLine("Renaming file {0} to {1}", file, newFile);
                File.Move(file, newFile);
            }
        }
    }
}