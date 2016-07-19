using System;
using System.IO;

namespace Ратновская_Ангелина_Task2
{
       internal class FileReader
        {
           internal string ReadFile(string path)
            {
                try
                {
                    var file = new StreamReader(path);

                    string line = file.ReadToEnd();
                    var fileInfo = new FileInfo(path);

                    // Если файл прочитан, вызывается событие Read.
                    if (fileInfo.Length == line.Length)
                    {
                        if (Read != null)
                        {
                            Read();
                        }
                    }
                    return line;
                }

                catch (FileNotFoundException)
                {
                    
                    Console.WriteLine($"Файл {Program.inputPath} не найден");
                    Console.ReadKey();
                    return null;
                }
            }
            internal event Action Read;
        }
    }

