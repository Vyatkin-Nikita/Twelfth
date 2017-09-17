using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twelfth
{
    /* 
     * Реализация сортировки слиянием (через рекурсию) и блочной сортировки одномерного массива. 
     * Сортировки проверяются на отсортированном, не сортированном и сортированном в обратном порядке массивах.
     * Алгоритмы сортировки взяты из свободных источников. 
    */
    class Program
    {
        static int[] TaskArray;//Искомый массив, который будет заполнен и отсортирован
        static int CompareCount = 0;//Счётчик количества сравнений
        static int TransferCount = 0;//Счётчик количества пересылок
        static int VvodProverka(int mogr = 0, int bogr = 0)//Проверка вводимых с клавиатуры чисел, mogr и bogr - минимально и максимально возможные значения числа
        {
            bool ok;
            int n;
            do
            {
                ok = int.TryParse(Console.ReadLine(), out n);
                if (!ok) { Console.WriteLine("Ошибка. Неверный формат данных. Повторите ввод."); continue; }//Если введённые данные - не целое число, то новая итерация цикла
                if ((n < mogr) && (bogr < mogr)) { Console.WriteLine("Ошибка. Число должно быть больше или равно {0} . Повторите ввод.", mogr); ok = false; continue; }//Если целое число меньше указанного предела, то новая итерация цикла
                if ((n < mogr) && (mogr != 0) || ((n > bogr) && (bogr != 0))) { Console.WriteLine("Ошибка. Число должно находится в пределах от {0} до {1} . Повторите ввод.", mogr, bogr); ok = false; }//Если число выходит за указанные границы, то новая итерация цикла

            } while (!ok);
            Console.WriteLine();
            return n;
        }
        static void ShowArray (int [] mas)//Вывод заданного одномерного массива в консоль
        {
            
            for (int i =0; i< mas.Length; i++)
            {
                Console.Write(mas[i] + " ");
            }
            Console.WriteLine("\n");
        }
        static int[] GeneratorChaosArray()//Генератор неотсортированного одномерного массива
        {
            Console.WriteLine("Введите размер неупорядоченного массива");
            int[] Mass = new int[VvodProverka(1)];
            Random rnd = new Random();
            for (int i = 0; i < Mass.Length; i++)
            {
                Mass[i] = rnd.Next(100);                                                           
            }
            return Mass;
        }
        static int[] GeneratorOrderArray()//Генератор отсортированного одномерного массива
        {
            Console.WriteLine("Введите размер упорядоченного массива");
            int[] Mass = new int[VvodProverka(1)];
            Random rnd = new Random();
            for (int i = 0; i < Mass.Length; i++)
            {
                Mass[i] = rnd.Next(100);
            }
            Array.Sort(Mass);
            return Mass;
        }
        static int[] GeneratorReverseArray()//Генератор отсортированного в обратном порядке одномерного массива
        {
            Console.WriteLine("Введите размер упорядоченного в обратном порядке массива");
            int[] Mass = new int[VvodProverka(1)];
            Random rnd = new Random();
            for (int i = 0; i < Mass.Length; i++)
            {
                Mass[i] = rnd.Next(100);
            }
            Array.Sort(Mass);
            Array.Reverse(Mass);
            return Mass;
        }
        static int[] MergeSort(int [] mas)//Сортировка слиянием
        {
            if (mas.Length == 1)
                return mas;
            int MidPoint = mas.Length / 2;//Точка, по которой массив делится на два равных субмассива
            return Merge(MergeSort(mas.Take(MidPoint).ToArray()), MergeSort(mas.Skip(MidPoint).ToArray()));//Рекурсивное деление всех субмассивов до единичных и их слияние
        }
        static int[] Merge(int[] MassLeft, int[] MassRight)//Слияние субмассивов (сортировка слиянием)
        {
            int Left = 0, Right = 0;//Количетсво перенесённых элементов из субмассивов в "слитый массив"
            int[] merged = new int[MassLeft.Length + MassRight.Length];//"Слитый" массив
            for (int i = 0; i < merged.Length; i++)
            {
                if (Right < MassRight.Length && Left < MassLeft.Length)
                {
                    if (MassLeft[Left] > MassRight[Right])
                     merged[i] = MassRight[Right++];
                    else
                        merged[i] = MassLeft[Left++];
                    
                }
                else
                {
                    if (Right < MassRight.Length)
                        merged[i] = MassRight[Right++];
                    else
                        merged[i] = MassLeft[Left++];
                }
                CompareCount++;
                TransferCount++;
            }
            return merged;
        }
        static int[] BucketSort(int[] mas)//Блочная сортировка
        {
            int maxValue = mas[0];
            int minValue = mas[0];

            for (int i = 1; i < mas.Length; i++)//Нахождение максимального и минимального значений
            {
                if (mas[i] > maxValue)
                    maxValue = mas[i];
                CompareCount++;

                if (mas[i] < minValue)
                    minValue = mas[i];
                CompareCount++;
            }

            List<int>[] bucket = new List<int>[maxValue - minValue + 1];//Создание "карманов"(они же корзины, блоки)

            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }

            for (int i = 0; i < mas.Length; i++)//Распределение элементов по блокам и их сортировка
            {
                bucket[mas[i] - minValue].Add(mas[i]);
                TransferCount++;
               
            }

            int position = 0;
            for (int i = 0; i < bucket.Length; i++)//Перезаписывание из блоков в массив
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        mas[position] = bucket[i][j];
                        position++;
                    }
                }
            }
            return mas;
        }
        static void ArrayMenu()//Меню выбора массива
        {

            Console.WriteLine("Выберите тип одномерного массива:\n1. Неотсортированный массив\n2. Отсортированный массив\n3. Отсортированный в обратном порядке массив\n4. Выход из программы");
            int i = VvodProverka(1, 4);
            switch (i)
            {
                case 1:
                    {
                        Console.Clear();
                        TaskArray = GeneratorChaosArray();
                        ShowArray(TaskArray);
                        SortMenu();
                    }
                    break;
                case 2:
                    {
                        Console.Clear();
                        TaskArray = GeneratorOrderArray();
                        ShowArray(TaskArray);
                        SortMenu();
                    }
                    break;
                case 3:
                    {
                        Console.Clear();
                        TaskArray = GeneratorReverseArray();
                        ShowArray(TaskArray);
                        SortMenu();
                    }
                    break;
                case 4:
                    Environment.Exit(0);
                    break;

            }
        }
        static void SortMenu()//Меню выбора метода сортировки
        {

            Console.WriteLine("Выберите метод сортировки:\n1. Сортировка слиянием\n2. Блочная сортировка\n3. Предыдущее меню\n4. Выход из программы");
            int i = VvodProverka(1, 4);
            switch (i)
            {
                case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Сортировка слиянием\nМассив до сортировки");
                        ShowArray(TaskArray);
                        TaskArray = MergeSort(TaskArray);
                        Console.WriteLine("Массив после сортировки");
                        ShowArray(TaskArray);
                        Console.WriteLine("Количество сравненией: {0}", CompareCount);
                        Console.WriteLine("Количество пересылок: {0}", TransferCount);
                        ArrayMenu();
                    }
                    break;
                case 2:
                    {
                        Console.Clear();
                        Console.WriteLine("Блочная сортировка\nМассив до сортировки");
                        ShowArray(TaskArray);
                        TaskArray = BucketSort(TaskArray);
                        Console.WriteLine("Массив после сортировки");
                        ShowArray(TaskArray);
                        Console.WriteLine("Количество сравненией: {0}", CompareCount);
                        Console.WriteLine("Количество пересылок: {0}", TransferCount);
                        ArrayMenu();
                    }
                    break;
                case 3:
                    {
                        Console.Clear();
                        TaskArray = null;
                        CompareCount = 0;
                        TransferCount = 0;
                        ArrayMenu();
                    }
                    break;
                case 4:
                    Environment.Exit(0);
                    break;

            }
        }
        static void Main(string[] args)
        {
            ArrayMenu();
            Console.ReadLine();
        }
    }
}
