using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodichka
{
    internal class Metodichka
    {
        public static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int cols2 = matrix2.GetLength(1);

            if (cols1 != rows2)
            {
                throw new ArgumentException("Размер матрицы не подходит под вычесления");
            }

            int[,] result = new int[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    int sum = 0;

                    for (int k = 0; k < cols1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }

                    result[i, j] = sum;
                }
            }

            return result;
        }
        static double[] CalculateAverageTemperatures(double[,] temperature)
        {
            double[] averageTemperatures = new double[12];
            for (int month = 0; month < 12; month++)
            {
                double sum = 0;
                for (int day = 0; day < 30; day++)
                {
                    sum += temperature[month, day];
                }
                averageTemperatures[month] = sum / 30;
            }
            return averageTemperatures;
        }
        static (int, int) CountVowelsAndConsonants(string input)
        {
            // для задания Упражнение 6.1 достаточно написать строки содержашие гласные и согласные
            List<char> vowels = new List<char>() { 'e', 'y', 'u', 'i', 'o', 'a', 'й', 'ё', 'у', 'е', 'ы', 'а', 'о', 'э', 'ю', 'и', 'я' };
            List<char> cons = new List<char>() { 'q', 'w', 'r', 't', 'p', 'l', 'k', 'j', 'h', 'g', 'f', 'd', 's', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 'ц', 'к', 'н', 'г', 'ш', 'щ', 'з', 'х', 'ж', 'ъ', 'д', 'л', 'р', 'п', 'в', 'ф', 'ч', 'с', 'м', 'т', 'ь', 'б' };

            int sumVowels = 0;
            int sumCons = 0;

            foreach (char c in input)
            {
                if (vowels.Contains(c))
                {
                    sumVowels++;
                }
                else if (cons.Contains(c))
                {
                    sumCons++;
                }
            }

            return (sumVowels, sumCons);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Задание 6.1");
            string path = "File.txt";
            path = path.ToLower();
            var (vowelCount, consonantCount) = CountVowelsAndConsonants(path);

            Console.WriteLine($"Количество гласных в файле равно = {vowelCount}");
            Console.WriteLine($"Количество согласных = {consonantCount}");
            Console.WriteLine("Задание 6.2");
            int[,] matrix1 =
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };
            int[,] matrix2 =
        {
                {10, 11, 12},
                {13, 14, 15},
                {16, 17, 18}
            };
            int[,] result = MultiplyMatrices(matrix1, matrix2);
            Console.WriteLine("Матрица 1:");
            PrintMatrix(matrix1);
            Console.WriteLine("Матрица 2:");
            PrintMatrix(matrix2);
            Console.WriteLine("Результат умножения:");
            PrintMatrix(result);

            Console.WriteLine("Задание 6.3");
            double[,] temperature = new double[12, 30];
            Random random = new Random();
            for (int month = 0; month < 12; month++)
            {
                for (int day = 0; day < 30; day++)
                {
                    temperature[month, day] = random.NextDouble() * 40;
                }
            }
            double[] averageTemperatures = CalculateAverageTemperatures(temperature);
            for (int month = 0; month < 12; month++)
            {
                Console.WriteLine($"Средняя температура для месяца {month + 1}: {averageTemperatures[month]}");
            }
            Array.Sort(averageTemperatures);
            Console.WriteLine("Отсортированный массив средних температур:");
            foreach (double temperatures in averageTemperatures)
            {
                Console.WriteLine(temperatures);
            }
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey();
            Console.WriteLine("Задание 6.3");

          
            Dictionary<string, double[]> monthlyTemperatures = new Dictionary<string, double[]>
        {
            { "Январь", new double[30] },
            { "Февраль", new double[30] },
            { "Март", new double[30] },
            { "Апрель", new double[30] },
            { "Май", new double[30] },
            { "Июнь", new double[30] },
            { "Июль", new double[30] },
            { "Август", new double[30] },
            { "Сентябрь", new double[30] },
            { "Октябрь", new double[30] },
            { "Ноябрь", new double[30] },
            { "Декабрь", new double[30] }
        };

            Random randomGenerator = new Random();
            foreach (var month in monthlyTemperatures.Keys)
            {
                for (int day = 0; day < 30; day++)
                {
                    monthlyTemperatures[month][day] = randomGenerator.NextDouble() * 40; 

                }
            }


            Dictionary<string, double> averageMonthlyTemperatures = CalculateAverageTemperatures(monthlyTemperatures);


            foreach (var month in averageMonthlyTemperatures.Keys)
            {
                Console.WriteLine($"Средняя температура для месяца {month}: {averageMonthlyTemperatures[month]}");
            }

           

            var sortedAverageTemperatures = new List<double>(averageMonthlyTemperatures.Values);
            sortedAverageTemperatures.Sort();

            Console.WriteLine("Отсортированный массив средних температур:");
            foreach (double temperature1 in sortedAverageTemperatures)
            {
                Console.WriteLine(temperature1);
            }

            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey();

        }
        static Dictionary<string, double> CalculateAverageTemperatures(Dictionary<string, double[]> monthlyTemperatures)
        {
            Dictionary<string, double> averageTemperatures = new Dictionary<string, double>();

            foreach (var month in monthlyTemperatures.Keys)
            {
                double totalTemperature = 0;
                foreach (var dailyTemperature in monthlyTemperatures[month])
                {
                    totalTemperature += dailyTemperature;
                }
                averageTemperatures[month] = totalTemperature / monthlyTemperatures[month].Length; 

            }

            return averageTemperatures;
        }
    }
}

