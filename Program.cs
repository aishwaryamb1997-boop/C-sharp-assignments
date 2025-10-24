using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

/*
 5.	Write a program to write 10MB of data to multiple files as per the procedure mentioned below:
•	Write 10MB of binary data in blocks of 50kB byte array
•	The above should be replicated for 100 files i.e. after execution, the program should have created 100 number 10MB files.
•	After completion of writing to each single file, the program should output the following message on console "<Filename> Writing Completed"
•	Execute the above program in a single thread and also multiple threads/tasks using async/await programming. Make sure that the UI thread is not blocked.
•	Note the time taken for execution of the program in single task and multiple tasks
 */

namespace C_TASK05_DataToFiles
{
    class Program
    {
        
        static readonly int FileSizeBytes = 10 * 1024 * 1024;  
        static readonly int BlockSizeBytes = 50 * 1024;        
        static readonly int FileCount = 100;                   
        static readonly string FolderPath = "OutputFiles";     

        static async Task Main()
        {
            
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            Console.WriteLine("===== SINGLE THREAD EXECUTION =====");
            var singleThreadTime = await RunSingleThreadAsync();
            Console.WriteLine($"Total time (Single Thread): {singleThreadTime.TotalSeconds:F2} seconds\n");

            Console.WriteLine("===== MULTI THREAD (ASYNC/TASK) EXECUTION =====");
            var multiThreadTime = await RunMultiThreadAsync();
            Console.WriteLine($"Total time (Multi Thread): {multiThreadTime.TotalSeconds:F2} seconds\n");

            Console.WriteLine(" Program completed. Press any key to exit.");
            Console.ReadKey();
        }

        // ------------------ SINGLE THREAD VERSION ------------------
        static async Task<TimeSpan> RunSingleThreadAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 1; i <= FileCount; i++)
            {
                string fileName = Path.Combine(FolderPath, $"File_{i}.bin");
                await WriteFileAsync(fileName); 
                Console.WriteLine($"{Path.GetFileName(fileName)} Writing Completed");
            }

            sw.Stop();
            return sw.Elapsed;
        }

        // ------------------ MULTI THREAD VERSION ------------------
        static async Task<TimeSpan> RunMultiThreadAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();

            
            Task[] tasks = new Task[FileCount];

            for (int i = 1; i <= FileCount; i++)
            {
                string fileName = Path.Combine(FolderPath, $"File_{i}.bin");
                tasks[i - 1] = Task.Run(async () =>
                {
                    await WriteFileAsync(fileName);
                    Console.WriteLine($"{Path.GetFileName(fileName)} Writing Completed");
                });
            }

            await Task.WhenAll(tasks); 

            sw.Stop();
            return sw.Elapsed;
        }

        
        static async Task WriteFileAsync(string filePath)
        {
            
            byte[] buffer = new byte[BlockSizeBytes];
            new Random().NextBytes(buffer);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, BlockSizeBytes, useAsync: true))
            {
                int blocksToWrite = FileSizeBytes / BlockSizeBytes;

                for (int i = 0; i < blocksToWrite; i++)
                {
                    await fs.WriteAsync(buffer, 0, buffer.Length); 
                }

                await fs.FlushAsync(); 
            }
        }
    }
}

