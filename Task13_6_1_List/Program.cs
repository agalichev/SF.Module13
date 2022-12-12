﻿using System.Diagnostics;

namespace Task13_6_1_List
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Text1.txt";

            int passes;

            //Проверяем существует ли файл
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            do
            {
                Console.Write("Необходимо ввести количество проходов: ");
            }
            while (!int.TryParse(Console.ReadLine(), out passes) || passes < 1);

            //Объявляем массив значений времени выполнения записи в List<T>
            var performance = new double[passes];

            //Прогоняем запись в List<T> заданное количество раз
            for(int i = 0; i < passes; i++)
            {
                performance[i] = WriteToList(path);
                Console.WriteLine($"Время записи за проход {i}: {performance[i]} мс");
            }

            //Вычисляем и выводим среднее арифметическое значение времени записи в List<T>
            Console.WriteLine($"Среднее значение времени записи: {performance.Sum() / passes} мс");
        }

        /// <summary>
        /// В методе выполняется замер времени записи строк в список List<T>, считанных из файла .txt
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Время выполнения записи в список</returns>
        static double WriteToList(string path)
        {
            var text = File.ReadLines(path);

            List<string> list = new List<string>();

            var watch = Stopwatch.StartNew();
            foreach(var line in text)
                list.Add(line);

            return watch.ElapsedMilliseconds;
        }
    }
}