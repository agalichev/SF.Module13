using System.Diagnostics;

namespace Task13_6_1_LinkedList
{
    /// <summary>
    /// Основной класс программы
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Text1.txt";

            int passes;

            double average;

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
            for (int i = 0; i < passes; i++)
            {
                performance[i] = WriteToLinkedList(path);
                Console.WriteLine($"Время записи за проход {i + 1}: {performance[i]} мс");
            }

            //Вычисляем и выводим среднее арифметическое значение времени записи в List<T>
            average = performance.Sum() / passes;
            Console.WriteLine($"Среднее значение времени записи: {average} мс");

            //Создаем файл и записываем туда результы всех прогонов и среднее значение
            var dir = Path.GetDirectoryName(Directory.GetCurrentDirectory());

            if (!Directory.Exists(dir))
                return;

            var fullname = Directory.GetParent(dir)!.FullName;
            var projectRoot = fullname.Substring(0, fullname.Length - 4);

            var filepath = Directory.GetParent(projectRoot)?.FullName + @"\linkedlistStat.txt";

            using (StreamWriter sw = File.CreateText(filepath))
            {
                Console.WriteLine("Файл создан. Производится запись данных.");
                for (int i = 0; i < performance.Length; i++)
                {
                    sw.WriteLine($"Время записи за проход {i + 1}: {performance[i]} мс");
                }

                sw.WriteLine($"Среднее значение времени записи: {average} мс");
            }
        }

        /// <summary>
        /// Метод выполняет замер времени записи строк в связанный список
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Время выполнения записи в список</returns>
       static double WriteToLinkedList(string path)
        {
            var text = File.ReadLines(path);

            LinkedList<string> linkedList = new LinkedList<string>();

            var watch = Stopwatch.StartNew();
            foreach (var line in text)
                linkedList.AddFirst(line);

            return watch.ElapsedMilliseconds;
        }
    }
}