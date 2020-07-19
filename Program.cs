using System;

namespace Курсова_робота
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Time.DoOrderBeforeBreak();
            Time.CanteensWork();
            Time.EndOfWork();           
                
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n\n    ~ A_Saker_K ~ ");
        }
    }
}