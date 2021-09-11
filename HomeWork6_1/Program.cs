using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace HomeWork_6
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime time = new DateTime(); //инициализация переменной time для получения текущего времени

            int n = 0;
            if (File.Exists("number.txt"))
            {
                using (StreamReader sr = new StreamReader("number.txt")) //чтение файла number.txt
                {
                    n = int.Parse(sr.ReadLine()); //n - исходное число для задачи
                }
            }
            else
            {
                Console.WriteLine("Отсутствует файл");
                return;
            }

            if (n < 0 || n > 1_000_000_000) //если исходное число не принадлежит промежутку от 0 до 1000000000, то
            {
                Console.WriteLine("Ошибка: введенное число не входит в промежуток от 0 до 1_000_000_000");
                return; //выход из программы
            }

            int measurement1 = 0;
            Console.WriteLine("Рассчитать группы или вывести количество групп?");
            Console.Write("Рассчитать группы - выберите 1, Посчитать количество групп - выберите 2: "); measurement1 = int.Parse(Console.ReadLine());
            switch (measurement1)
            {
                case 1: //расчет групп
                    List<int> numbers = new List<int>();
                    List<List<int>> groups = new List<List<int>>();
                    numbers = Enumerable.Range(1, n).ToList();
                    time = DateTime.Now; //время начала расчетов
                    List<string> group = new List<string>();
                    //StringBuilder str = new StringBuilder();
                    string str = "";
                    int j = 1;
                    int k = 0;
                    for (int i = 1; i <= n; i++)
                    {
                        k++;
                        str += $"{i} ";
                        //str.Insert(k, i);
                        if ((i == (int)Math.Pow(2, j) - 1) || (i == n))
                        {
                            //group.Add(str.ToString());
                            group.Add(str);
                            //str.Remove(0, 1);
                            //str = "";
                            j++;
                            //k = 0;
                        }
                    }
                    TimeSpan timeSpan = DateTime.Now.Subtract(time); //время окончания расчетов
                    Console.WriteLine($"Время: {timeSpan.TotalMinutes}"); //вывод времени, потраченного на работу программы для расчета групп
                    Console.WriteLine();
                    Console.WriteLine("Подождите, пока группы запишутся в файл");
                    using (StreamWriter sw = new StreamWriter("text.txt")) //запись групп в файл
                    {
                        int i = 1;
                        foreach (var e in group)
                        {
                            sw.Write($"Группа {i}: {e} ");
                            sw.WriteLine("\t");
                            i++;
                        }
                    }
                    Console.WriteLine("Группы записаны");
                    Console.WriteLine();
                    int measurement2 = 0;
                    Console.Write("Заархивировать файл? 'Да' - выберите 1, 'Нет' - выберите 2: "); measurement2 = int.Parse(Console.ReadLine());
                    switch (measurement2)
                    {
                        case 1: //архивирование полученного файла
                            string source = "text.txt";
                            string compressed = "text.zip";
                            using (FileStream ss = new FileStream(source, FileMode.OpenOrCreate))
                            {
                                using (FileStream ts = File.Create(compressed))
                                {
                                    using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
                                    {
                                        ss.CopyTo(cs);
                                        Console.WriteLine("Сжатие файла {0} завершено. Было {1}, стало {2}",
                                            source,
                                            ss.Length,
                                            ts.Length);
                                    }
                                }
                            }
                            break;
                        case 2: //при отказе от архивирования - выход из программы
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine($"Количество групп: {Math.Truncate(Math.Log(n, 2)) + 1}"); //вывод количества групп
                    break;
                default:
                    break;
            }
        }
    }
}