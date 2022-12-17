using System.Linq;

namespace Task13_6_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Указываем путь к файлу
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Text1.txt";

            //Проверяем, существует ли файл по указанному пути
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!\nЗавершение работы приложения!");
                return;
            }

            //Чмтаем файл и переносим текст в переменную
            var text = File.ReadAllText(filePath);

            //Удаляем всю пунктуацию из текста
            var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

            //Разбиваем текст на отдельные слова и заносим их в массив, откинув разделители и цифры
            char[] delimiters = new char[] { ' ', '\r', '\n', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var words = noPunctuationText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> dr = new Dictionary<string, int>();

            //Переносим слова из массива в словарь и проверяем их на повтор, увеличивая счетчик(значение) при положительном результате
            foreach(var s in words)
            {
                if (dr.Keys.Contains(s)) dr[s]++;
                else dr.Add(s, 1);
            }

            string output = "";
            int k = 0;

            //Перебираем содержимое словаря, предварительно отсортировав его по убыванию значений, и выбираем 10 слов
            foreach (KeyValuePair<string, int> item in dr.OrderByDescending(x => x.Value))
            {
                output += item.Key + " " + item.Value.ToString() + "\n";
                k++;
                if (k == 10)
                    break;
            }

            Console.WriteLine(output);

            Console.ReadLine();
        }
    }
}