using System;

namespace Ратновская_Ангелина_Task2
{
    class Program
    {
        internal const string inputPath = @"D:\input.txt";
        internal const string outputPath = @"D:\output.txt";
       
        const string separator = ";";

       internal static string[] GetArray(string str)
        {
           string[] separators = { separator };
           return str.Split(separators, StringSplitOptions.None);
        }

        static void Main()
        {           
            FileReader reader = new FileReader();         

            // Подписываемся на события чтения и записи.                       
            reader.Read += () => { Console.WriteLine("Чтение файла завершено"); };
            FileWriter writer = new FileWriter();
            writer.Written += () => { Console.WriteLine("Запись файла завершена"); };

            string str = reader.ReadFile(inputPath);
            
            string[] words = GetArray(str);

            var result = ArrayHandler.HandleArray(words);

            string message = $"Арифметическая сумма = {result.Item1}\r\nЧисло символов = {result.Item2}";

            // Записываем результат в файл. 
            writer.WriteFile(outputPath, message);

            Console.ReadKey();
        }
    }
}