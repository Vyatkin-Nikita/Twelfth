using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twelfth
{
    class Program
    {
        int[] TaskArray;
        static int VvodProverka(int mogr = 0, int bogr = 0)//Проверка вводимых с клавиатуры чисел, mogr и bogr - минимально и максимально возможные значения числа
        {
            bool ok;
            int n;
            do
            {
                ok = int.TryParse(Console.ReadLine(), out n);
                if (!ok) { Console.WriteLine("Ошибка. Неверный формат данных. Повторите ввод."); continue; }
                if ((n < mogr) && (bogr < mogr)) { Console.WriteLine("Ошибка. Число должно быть больше или равно {0} . Повторите ввод.", mogr); ok = false; continue; }
                if ((n < mogr) && (mogr != 0) || ((n > bogr) && (bogr != 0))) { Console.WriteLine("Ошибка. Число должно находится в пределах от {0} до {1} . Повторите ввод.", mogr, bogr); ok = false; }

            } while (!ok);
            Console.WriteLine();
            return n;
        }
        static int [] GeneratorChaosArray()
        {
            Console.WriteLine("Введите размер неупорядоченного массива");
            int[] Mass = new int[VvodProverka(1)];
            Random rnd = new Random();
            for (int i = 0; i < Mass.Length; i++)
            {
                Mass[i] = rnd.Next();                                                           
            }
            return Mass;
        }
        static int[] GeneratorOrderArray()
        {
            Console.WriteLine("Введите размер упорядоченного массива");
            int[] Mass = new int[VvodProverka(1)];
            Random rnd = new Random();
            for (int i = 0; i < Mass.Length; i++)
            {
                Mass[i] = rnd.Next();
            }
            Array.Sort(Mass);
            return Mass;
        }
        static int[] GeneratorReverseArray()
        {
            Console.WriteLine("Введите размер упорядоченного в обратном порядке массива");
            int[] Mass = new int[VvodProverka(1)];
            Random rnd = new Random();
            for (int i = 0; i < Mass.Length; i++)
            {
                Mass[i] = rnd.Next();
            }
            Array.Sort(Mass);
            Array.Reverse(Mass);
            return Mass;
        }
        static int[] MergeSort(int [] mas)
        {
            if (mas.Length == 1)
                return mas;
            int MidPoint = mas.Length / 2;
            return Merge(MergeSort(mas.Take(MidPoint).ToArray()), MergeSort(mas.Skip(MidPoint).ToArray()));
        }
        static int[] Merge(int[] MassLeft, int[] MassRight)
        {
            int Left = 0, Right = 0;
            int[] merged = new int[MassLeft.Length + MassRight.Length];
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
            }
            return merged;
        }
        static void Main(string[] args)
        {
        }
    }
}
