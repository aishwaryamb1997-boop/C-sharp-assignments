using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C__Task01_output.txt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string folderPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyFiles");

                string filePath = Path.Combine(folderPath, "output.txt");

                if(!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Console.WriteLine($"created new folder @ {folderPath}");
                }
                Console.WriteLine("Enter multiple lines of text (type 'STOP' to finish):");

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    while(true)
                    {
                        string inputLine = Console.ReadLine();
                        if (inputLine.ToUpper() == "STOP")
                            break;

                        writer.WriteLine(inputLine);
                    }
                }
                Console.WriteLine($"\n Text has been saved @ {filePath}");

                Console.WriteLine("Contents of the file:");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string fileContent = reader.ReadToEnd();
                    Console.WriteLine("------------------------------");
                    Console.WriteLine(fileContent);
                    Console.WriteLine("------------------------------");
                }
             
            }
            catch (IOException ioEx)
            {
                Console.WriteLine(ioEx.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
