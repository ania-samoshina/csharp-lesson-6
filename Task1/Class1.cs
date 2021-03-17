using System;


namespace Task1
{
    //1. Изменить программу вывода функции так, чтобы можно было передавать функции типа double (double,double). 
    //Продемонстрировать работу на функции с функцией a*x^2 и функцией a*sin(x).


    // Описываем делегат. В делегате описывается сигнатура методов, на
    // которые он сможет ссылаться в дальнейшем (хранить в себе)
    public delegate double Fun(double a, double x);

    public class Program
    {
        // Создаем метод, который принимает делегат
        // На практике этот метод сможет принимать любой метод
        // с такой же сигнатурой, как у делегата
        public static void Table(Fun F, double a, double x, double b)
        {
            Console.WriteLine("--------a----- X ----- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} | {2,8:0.000} |",a, x, F(a,x));
                x += 1;
            }
            Console.WriteLine("---------------------");
        }
        // Создаем метод для передачи его в качестве параметра в Table
        public static double MyFunc(double a, double x)
        {
            return a * Math.Pow(x,2);
        }

        public static double MySinFunc(double a, double x)
        {
            return a * Math.Sin(x);
        }

        public static void Main()
        {
            // Создаем новый делегат и передаем ссылку на него в метод Table
            Console.WriteLine("Function table MyFunc:");
            // Параметры метода и тип возвращаемого значения, должны совпадать с делегатом
            Table(new Fun(MyFunc), 5, -2, 2);
            Console.WriteLine("Once again the same table, but the call is organized in a new way");
            // Упрощение(c C# 2.0).Делегат создается автоматически.            
            Table(MyFunc, 5, -2, 2);
            Console.WriteLine("Function table a*Sin(x):");
            Table(MySinFunc, 2, 1, 2);      // Можно передавать уже созданные методы
            Console.WriteLine("Function table a*x^2:");
            // Упрощение(с C# 2.0). Использование анонимного метода
            Table(delegate (double a, double x) { return a * Math.Pow(x, 2); }, 2, 1, 2);
        }
    
}
}
