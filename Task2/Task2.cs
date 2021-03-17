    //2. Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
    //а) Сделайте меню с различными функциями и предоставьте пользователю выбор, для какой функции и на каком отрезке находить минимум.
    //б) Используйте массив(или список) делегатов, в котором хранятся различные функции.
    //в) * Переделайте функцию Load, чтобы она возвращала массив считанных значений.Пусть она
    //возвращает минимум через параметр.





using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Task2
{
    /// <summary>Делегат функции с сигнатурой double, double</summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public delegate double MathFunc(double x);

    public class Task2
    {
        /// <summary>Метод производит расчёт значения переданной функции и записывает в файл двоинчм потоком</summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="mathFuncResult">Начальное значение аргумента</param>
        public static void SaveFunc(string fileName, IEnumerable<double> mathFuncResult)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {

                foreach (double funcResult in mathFuncResult)
                {
                    bw.Write(funcResult);
                }
            }
        }

        /// <summary>Функция возвращает массив значений из файла и находит минимальное</summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Коллекция результатов выполнения функции</returns>
        public static double[] Load(string fileName, out double min)
        {
            double[] array;

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (BinaryReader bw = new BinaryReader(fs))
            {
                array = new double[fs.Length / sizeof(double)];

                for (int i = 0; i < fs.Length / sizeof(double); i++)
                {
                    // Считываем значение и переходим к следующему
                    array[i] = bw.ReadDouble();
                }
            }

            min = FindMinimum(array);

            return array;
        }

        /// <summary>
        /// Метод находит минимальное значение в коллекции результатов выполнения функции
        /// </summary>
        /// <param name="mathFuncResults">Коллекция результатов выполнения функции</param>
        /// <returns>Минимальное значение</returns>
        public static double FindMinimum(IEnumerable<double> mathFuncResults) => 
             mathFuncResults.Min(x => x);

        /// <summary>Функция возведения в квадрат</summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double secondDegree(double x)
        {
            return x * x;
        }

        /// <summary>Функция возведения в третью степень</summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double thirdDegree(double x)
        {
            return x * x * x;
        }

        /// <summary>Функция квадратного корня</summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double mySqrt(double x)
        {
            return Math.Sqrt(x);
        }

        /// <summary>Функция синуса</summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static double Sin(double x)
        {
            return Math.Sin(x);
        }

        /// <summary>Метод проверяет ввод целочисленного значения, меньше заданого</summary>
        /// <param name="max">Максимальное значение, которое может ввести пользователь</param>
        /// <returns></returns>
        static int GetInt(int max)
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int x) || x > max)
                    Console.Write("Неверный ввод (требуется числовое значение от 0 до {0}).\nПожалуйста повторите ввод: ", max);
                else return x;
        }

        /// <summary>Метод получает значения начала отрезка и конца из одной строки</summary>
        /// <param name="start">Начало отрезка</param>
        /// <param name="end">Конец отрезка</param>
        static void GetInterval(out double start, out double end)
        {
            string[] interval = Console.ReadLine().Split(' ');
            start = double.Parse(interval[0], CultureInfo.InvariantCulture);
            end = double.Parse(interval[1], CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Метод производит расчёт значения переданной функции
        /// </summary>
        /// <param name="mathFunc">Делегат функции</param>
        /// <param name="start">Начальное значение аргумента</param>
        /// <param name="end">Конечное значение агрумента</param>
        /// <param name="step">Шаг дисретизации</param>
        /// <returns>Коллекцию результов расчёта переданной функции</returns>
        static IEnumerable<double> RunFunc(MathFunc mathFunc, double start, double end, double step)
        {
            List<double> results = new List<double>();
            double currentArgument = start;

            while (currentArgument <= end)
            {
                results.Add(mathFunc(currentArgument));
                currentArgument += step;
            }

            return results;
        }

        /// <summary>Метод выводит на экран значение функции и её аргумента</summary>
        /// <param name="start">Начальное значенеи аргумента</param>
        /// <param name="end">Конечное значение аргумента</param>
        /// <param name="step">Шаг дискредитирования</param>
        /// <param name="values">Массив значений функции</param>
        static void PrintResults(double start, double end, double step, IEnumerable<double> values)
        {
            Console.WriteLine("------- X ------- Y -----");
            foreach(double value in values)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} ", start, value);
                start += step;
            }
            Console.WriteLine("--------------------------");
        }

        public static void Execute()
        {
            List<MathFunc> functions = new List<MathFunc> { new MathFunc(secondDegree), new MathFunc(thirdDegree), new MathFunc(mySqrt), new MathFunc(Sin) };
            Console.WriteLine("Select the function to find the minimum of the function.");
            Console.WriteLine("1) f(x)=y^2");
            Console.WriteLine("2) f(x)=y^3");
            Console.WriteLine("3) f(x)=y^1/2");
            Console.WriteLine("4) f(x)=Sin(y)");
            int userChoose = GetInt(functions.Count);

            Console.WriteLine("Set the segment to find the minimum in the format 'х1 х2':");

            double start = 0;
            double end = 0;
            GetInterval(out start, out end);

            Console.WriteLine("Set the step size:");
            double step = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            IEnumerable<double> funcRunResults = RunFunc(functions[userChoose - 1], start, end, step);

            SaveFunc("data.bin", funcRunResults);
            Console.WriteLine("The following function values are obtained: ");
            double min = double.MinValue;
            funcRunResults = Load("data.bin", out min);
            PrintResults(start, end, step, funcRunResults);
            Console.WriteLine("The minimum value of the function is: {0:0.00}", min);
            Console.ReadKey();
        }
    }
}
