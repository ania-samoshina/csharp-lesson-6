using System;

namespace Print
{
    public class Class1
    {
        public static void PrintName(string mess)
        {
            int x = Console.WindowWidth;
            var center = x / 2 + mess.Length / 2;
            Console.WriteLine("{0," + center + "}", mess);
        }
    }
}
