using System;

namespace Course6
{
    class Program
    {
        static void Main(string[] args)
        {
            Print.Class1.PrintName("Ann Samoshina");
            Console.WriteLine("Enter your task: ");
            int caseSwitch = Convert.ToInt32(Console.ReadLine());
            switch (caseSwitch)
            {
                case 1:
                    Task1.Program.Main();
                    break;
                case 2:
                    Task2.Task2.Execute();
                    break;
                case 3:
                    Task3.Task3.Execute();
                    break;
            }
            Console.ReadKey();

        }
    }
}