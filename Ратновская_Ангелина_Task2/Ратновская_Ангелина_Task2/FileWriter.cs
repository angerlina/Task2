using System;
using System.IO;

namespace Ратновская_Ангелина_Task2
{
   internal class FileWriter
    {
        internal void WriteFile(string path, string str)
        {
            try
            {
                File.WriteAllText(path, str);
            }

            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Недостаточно прав для записи файла");
                return;
            }

            var file = new StreamReader(path);
            var fileStr = file.ReadToEnd();

            // если файл успешно записался, вызывается событие Written 
            if (fileStr != str) return;
            if (Written != null)
            {
                Written();
            }
        }

        internal event Action Written;
    }
}
