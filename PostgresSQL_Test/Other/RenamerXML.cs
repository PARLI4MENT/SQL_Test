﻿#define TEST1

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace SqlTest
{
    public class RenamerXML
    {
        [NotNull]
        static string _rootDir = "C:\\_test";
        public static string RootDir { get { return _rootDir; } set { _rootDir = value; } }

        private static string _dirInput = Path.Combine(_rootDir, "rawFiles");

        private static string _dirDestination = Path.Combine(_rootDir, "inputFiles");

        /// <summary>
        /// Move & rename xml`s files by mask
        /// </summary>
        /// <param name="rawFile">Путь к папке с исходными файлами</param>
        /// <param name="destinationPath">Путь к папке назначения</param>
        /// <param name="deletedRawFolder">Удалять исходную папку</param>
        public void RenameAndMove([Optional] string rawFile, [Optional] string destinationPath, bool deletedRawFolder = false)
        {
            Console.WriteLine("Start rename && move...");

            if (!string.IsNullOrEmpty(rawFile))
                _dirInput = rawFile;

            if (!string.IsNullOrEmpty(destinationPath))
                _dirDestination = destinationPath;

            // Timer
            var sw = new Stopwatch();
            sw.Start();

            var baseFolder = new List<string>();
            baseFolder.AddRange(Directory.GetDirectories(_dirInput).ToList<string>());

#if TEST
            int index = 0;
#endif
            int subFolder = 0;
            foreach (var dir in baseFolder)
            {
#if TEST
                Console.WriteLine($"[{index}] => {dir}");
#endif

                /// Тут можно упростить => 
                string tmpSubfolder = Path.GetFileName(dir);
                string[] subDir = Directory.GetDirectories(Path.Combine(dir, "files"));

                List<string> filesSubfolder = new List<string>();
                filesSubfolder.AddRange(Directory.GetFiles(Path.Combine(subDir[0], "xml")));
#if TEST
#endif
                foreach (string file in filesSubfolder)
                {
#if TEST
                    Console.WriteLine($"\t[{subIndex}] => {Path.GetFileName(file)}");
#endif
                    subFolder += filesSubfolder.Count;
                    Task.Run(() =>
                        File.Copy(file, Path.Combine(_dirDestination, string.Concat(tmpSubfolder, ".", Path.GetFileName(file)))));
#if TEST
                    subIndex++;
#endif
                }
            }

            // Timer
            sw.Stop();

            Console.WriteLine("{");
            Console.WriteLine("\tOperation => ParseXMLByMask is DONE!");
            Console.WriteLine($"\tBase folder = {baseFolder.Count}");
            Console.WriteLine($"\tParsed files = {subFolder}");
            Console.WriteLine($"\tTotal time: [{sw.Elapsed}]");
            Console.WriteLine("{\n");

            // Delete non usable base folder
            if (deletedRawFolder)
                Task.Run(() => Delete());
        }

        /// <summary>
        /// Parallel move & rename xml`s files by mask
        /// </summary>
        /// <param name="rawFile">Путь к папке с исходными файлами</param>
        /// <param name="destinationPath">Путь к папке назначения</param>
        /// <param name="deletedRawFolder">Удалять исходную папку</param>
        public void RenameAndMoveParallel([Optional] string rawFile, [Optional] string destinationPath, bool deletedRawFolder = false)
        {
            Console.WriteLine("Start rename && move...");
            var swRename = new Stopwatch();
            swRename.Start();

            if (!string.IsNullOrEmpty(rawFile))
                _dirInput = rawFile;

            if (!string.IsNullOrEmpty(destinationPath))
                _dirDestination = destinationPath;

            // Timer
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            // Переписать на string[]
            var baseFolder = new List<string>();
            baseFolder.AddRange(Directory.GetDirectories(_dirInput).ToList<string>());

#if TEST
            int index = 0;
#endif
            int subFolder = 0;
            Parallel.ForEach(baseFolder, dir =>
            {
#if TEST
                Console.WriteLine($"[{index}] => {dir}");
#endif

                /// Тут можно упростить => 
                string tmpSubfolder = Path.GetFileName(dir);
                string[] subDir = Directory.GetDirectories(Path.Combine(dir, "files"));

                List<string> filesSubfolder = new List<string>();
                filesSubfolder.AddRange(Directory.GetFiles(Path.Combine(subDir[0], "xml")));


                foreach (string file in filesSubfolder)
                {
#if TEST
                    Console.WriteLine($"\t[{subIndex}] => {Path.GetFileName(fie)}");
#endif
                    subFolder += filesSubfolder.Count;
                    Task.Run(() =>
                        File.Copy(file, Path.Combine(_dirDestination, string.Concat(tmpSubfolder, ".", Path.GetFileName(file))))
                    );
                    //RenameFile(file, tmpSubfolder);
#if TEST
                    subIndex++;
#endif
                }
            });

            // Timer
            sw.Stop();

            //Console.WriteLine($"\tParsed files = {subFolder}");

            swRename.Stop();
            Console.WriteLine("{");
            Console.WriteLine("\tOperation => ParseXMLByMaskParallel is DONE!");
            Console.WriteLine($"\nTotal time: {swRename.ElapsedMilliseconds},{swRename.ElapsedMilliseconds%1000} msec");
            Console.WriteLine($"\tRaw folder with files: {baseFolder.Count}");
            Console.WriteLine($"Total files ({Directory.GetFiles("C:\\_test\\inputFiles").Count()} " +
                $"/ {Directory.GetFiles("C:\\_test\\rawFiles").Count()})");
            Console.WriteLine($"AVG time: {Directory.GetFiles("C:\\_test\\inputFiles").Count() / (int)(swRename.ElapsedMilliseconds / 1000)},"
                 + $"{swRename.ElapsedMilliseconds % 1000} units");
            Console.WriteLine("}\n");

            // Delete non usable base folder
            if (deletedRawFolder)
                Task.Run(() => Delete());
        }

        /// <summary>
        /// Deleted raw folder
        /// </summary>
        private void Delete()
        {
            foreach (var item in Directory.GetDirectories(_dirInput))
            {
                Directory.Delete(item);
            }
        }


        /// <summary>
        /// Для личных нужд
        /// </summary>
        /// <param name="lists"></param>
        protected static void OutFilePath(List<string> lists)
        {
            foreach (object item in lists)
            {
                Console.WriteLine($"[] => {item}");
            }
        }
    }
}
