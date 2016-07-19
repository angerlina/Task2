using System;

namespace Ратновская_Ангелина_Task2
{
   internal class ArrayHandler
    {

       internal delegate double Function<T>(T item, double sum);

       internal static double GetArithmeticSum(double z, double sum1)
        {
            sum1 += z;
            return sum1;
        }

       internal static double GetNumberOfCharacters(string str, double sum2)
        {
            sum2 += str.Length;
            return sum2;
        }

       internal static double HandleItem<T>(Function<T> DoSomething, T item, double sum)
        {
            return DoSomething(item, sum);
        }

        internal static Tuple<double, double> HandleArray(string[] words)
        {
            double sum1 = 0;
            double sum2 = 0;

            foreach (var word in words)
            {
                double number;
                // Если в массиве попалось число, передаем параметром метод для чисел. 
                if (Double.TryParse(word, out number))
                {

                    sum1 = HandleItem(GetArithmeticSum, number, sum1);

                }
                // Если в массиве - не число, передаем параметром метод для строк. 
                else
                {
                    sum2 = HandleItem(GetNumberOfCharacters, word, sum2);
                }
            }

            return new Tuple<double, double>(sum1, sum2);

        }


    }
}
