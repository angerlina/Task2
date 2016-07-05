using System;

namespace ConsoleApplication1
{
    class Program
    {

        public delegate double Function<T>(T item, double sum);

        // Метод для сложения чисел. 
        static double ArithmeticSum(double z, double sum1)
        {
            sum1 += z;
            return sum1;
        }

        // Метод для складывания числа символов. 
        static double NumberOfCharacters(string str, double sum2)
        {
            sum2 += str.Length;
            return sum2;
        }

        // Метод, в который передается универсальный делегат.
        static double Method<T>(Function<T> SomeMethod, T item, double sum1)
        {
            return SomeMethod(item, sum1);
        }

        // Класс, содержащий метод для чтения файла и событие, возникающее, в случае, когда файл прочитан. 
        class FileReader
        {
            // Метод для чтения файла. 
            public string ReadingFileMethod(string path)
            {
                var file = new System.IO.StreamReader(path);

                string line = file.ReadToEnd();
                var fileInfo = new System.IO.FileInfo(path);
                //Если файл прочитан, вызывается событие Read.
                if (fileInfo.Length == line.Length)
                {
                    if (Read != null)
                    {
                        Read();
                    }
                }
                return line;
            }

            public event Action Read;
        }

        // Класс, записывающий файлы. 
        class FileWriter
        {
            // Метод для записи файла. 
            public void WritingFileMethod(string path, string str)
            {
                System.IO.File.WriteAllText(path, str);

                var file = new System.IO.StreamReader(path);
                var fileStr = file.ReadToEnd();
                // если файл успешно записался, вызывается событие Written 
                if (fileStr == str)
                {
                    if (Written != null)
                    {
                        Written();
                    }
                }
            }

            public event Action Written;
        }

        static void Main()
        {
            const string separator = ";";

            // Подписываемся на события чтения и записи. 
            FileReader reader = new FileReader();            
            reader.Read += () => { Console.WriteLine("Чтение файла завершено"); };

            FileWriter writer = new FileWriter();
            writer.Written += () => { Console.WriteLine("Запись файла завершена"); };

            // Читаем файл, делаем из него массив.
            string[] separators = { separator };
            string[] words = reader.ReadingFileMethod(@"D:\input.txt").Split(separators, StringSplitOptions.None);

            // Обрабатываем полученный массив. 
            double sum1 = 0;
            double sum2 = 0;
            foreach (var word in words)
            {
                double number;
                // Если в массиве попалось число, передаем параметром метод для чисел. 
                if (Double.TryParse(word, out number))
                {

                    sum1 = Method(ArithmeticSum, number, sum1);

                }
                // Если в массиве - не число, передаем параметром метод для строк. 
                else
                {
                    sum2 = Method(NumberOfCharacters, word, sum2);
                }
            }

            // Создаем строку для записи в файл. 
            string result = String.Format("Арифметическая сумма = {0}\r\nЧисло символов = {1}", sum1, sum2);

            // Записываем ее в файл. 
            writer.WritingFileMethod(@"D:\output.txt", result);

            Console.ReadKey();
        }
    }
}