using System;

namespace ConsoleApplication1
{
    class Program
    {

        public delegate double Function<T>(T item, double sum);

        //метод для сложения чисел 
        static double ArithmeticSum(double z, double sum1)
        {
            sum1 += z;
            return sum1;
        }

        //метод для складывания числа символов 
        static double NumberOfCharacters(string str, double sum2)
        {
            sum2 += str.Length;
            return sum2;
        }

        //метод, в который передается универсальный делегат 
        static double Method<T>(Function<T> SomeMethod, T item, double sum1)
        {
            return SomeMethod(item, sum1);
        }

        //класс, содержащий метод для чтения файла и событие, возникающее, в случае, когда файл прочитан 
        class FileReader
        {
            //метод для чтения файла 
            public string ReadingFileMethod(string path)
            {
                var file = new System.IO.StreamReader(path);

                string line = file.ReadToEnd();
                var fileInfo = new System.IO.FileInfo(path);
                //если файл прочитан, вызывается событие Read 
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

        //класс, записывающий файлы 
        class FileWriter
        {
            //метод для записи файла 
            public void WritingFileMethod(string path, string str)
            {
                System.IO.File.WriteAllText(path, str);

                var file = new System.IO.StreamReader(path);
                var fileStr = file.ReadToEnd();
                //если файл успешно записался, вызывается событие Written 
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

        //обработчик события, что файл прочитался 
        public class ReadingHandler
        {
            public static void Message()
            {
                Console.WriteLine("Чтение файла завершено");
            }
        }
        //обработчик события, что файл записался 
        public class WritingHandler
        {
            public static void Message()
            {
                Console.WriteLine("Запись в файл завершена");
            }
        }

        static void Main()
        {
            //создаем экземпляр обработчика события для чтения, подписываемся на событие 
            FileReader reader = new FileReader();
            ReadingHandler handler1 = new ReadingHandler();
            reader.Read += ReadingHandler.Message;

            //создаем экземпляр обработчика события для записи, подписываемся на событие 
            FileWriter writer = new FileWriter();
            WritingHandler handler2 = new WritingHandler();
            writer.Written += WritingHandler.Message;

            //читаем файл, делаем из него массив, разделяя знаком ";" 
            string[] separators = { ";" };
            string[] words = reader.ReadingFileMethod(@"D:\input.txt").Split(separators, StringSplitOptions.None);

            //обрабатываем полученный массив 
            double sum1 = 0;
            double sum2 = 0;
            foreach (var word in words)
            {
                double number;
                //если в массиве попалось число, передаем параметром метод для чисел 
                if (Double.TryParse(word, out number))
                {

                    sum1 = Method(ArithmeticSum, number, sum1);

                }
                //если в массиве - не число, передаем параметром метод для строк 
                else
                {
                    sum2 = Method(NumberOfCharacters, word, sum2);
                }
            }

            //создаем строку для записи в файл 
            var result = String.Format("Арифметическая сумма = {0}\r\nЧисло символов = {1}", sum1, sum2);
            //записываем ее в файл 
            writer.WritingFileMethod(@"D:\output.txt", result);

            Console.ReadKey();
        }
    }
}