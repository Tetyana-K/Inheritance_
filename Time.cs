using System;
using System.Collections.Generic;
using System.Text;

namespace Курсова_робота
{
    static class Time
    {
        static TimeSpan time = new TimeSpan(11, 30, 0);
        delegate void RememberPrintAll(string fname);
        static public void DoOrderBeforeBreak()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Statictic.PrintFile("../../../FullFile.xml");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            TimeSpan m = new TimeSpan(0, 1, 0);
            while (time.Hours != 12)
            {
                ConsoleKeyInfo key;
                RememberPrintAll Print = null;
                do
                {
                    if (time.Hours == 12) break;
                    Console.Write($"--------Wellcome to shcool--------               ");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{ time.Hours}:{ time.Minutes}\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("Hurry to order lunch, it`s start on 12:00 !!!");

                    Console.WriteLine("Press <<ENTER>> to do order or <<BACKSPACE>> to show menu");
                    Console.WriteLine("Press << + >> to add some dish to menu\n");

                    if (Print != null) Print("../../../FullFile.xml");
                    time += m;
                    System.Threading.Thread.Sleep(1000);
                    if (Console.KeyAvailable == false) Console.Clear();
                    else
                    {
                        key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                        {
                            if (Print != null) Console.Write("\n");
                            Console.Write("Enter id of dish ---> ");
                            int id = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter count of this ---> ");
                            int count = Convert.ToInt32(Console.ReadLine());

                            Chef.AddDishesToCanteen(id, count);
                            Console.Clear();
                            Print = null;
                        }
                        else if (key.Key == ConsoleKey.OemPlus)
                        {
                            Chef.AddDishToFullFile();
                            Console.Clear();
                        }
                        else if (key.Key == ConsoleKey.Backspace)
                        {
                            if (Print == null)
                            {
                                Console.Clear();
                            }
                            Print = new RememberPrintAll(Statictic.PrintFile);
                        }
                        else { Console.Clear(); Print = null; time -= m; }
                    }
                } while (Console.KeyAvailable == false);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"  { time.Hours} : { time.Minutes}. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
        static public void CanteensWork()
        {
            TimeSpan m = new TimeSpan(0, 1, 0);
            RememberPrintAll Print = null;
            while (time.Minutes != 30)
            {
                ConsoleKeyInfo key;
                do
                {
                    Console.Write("            TIME TO EAT ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"           { time.Hours} : { time.Minutes}. \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Hurry to eat, due to next lesson start on 12:30");
                    if (time.Minutes == 30) break;                  

                    Console.WriteLine("Press <<ENTER>> to do by dish or <<BACKSPACE>> to show Canteens`s menu\n");

                    if (Print != null) Print("../../../Canteen.xml");
                    time += m;
                    System.Threading.Thread.Sleep(1000);
                    if (Console.KeyAvailable == false) Console.Clear();
                    else
                    {
                        key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                        {
                            if (Print != null) Console.Write("\n");
                            Console.Write("Enter name of dish ---> ");
                            string name = Console.ReadLine();

                            Console.Write("Enter count of this ---> ");
                            int count = Convert.ToInt32(Console.ReadLine());

                            double price = 0;
                            bool rez = Chef.MakeAnOrder(ref price, name, count);
                            if (rez == true)
                            {
                               Statictic.AddSalesStatistics(price, name, count);
                                Chef.EvaluatingDish(name);
                            }
                            System.Threading.Thread.Sleep(3000);
                            Console.Clear();
                            Print = null;
                        }
                        else if (key.Key == ConsoleKey.Backspace)
                        {
                            if (Print == null)
                            {
                                Console.Clear();
                            }
                            Print = new RememberPrintAll(Statictic.PrintFile);
                        }
                        else { Console.Clear(); Print = null; time -= m; }
                    }
                } while (Console.KeyAvailable == false);
            }
            Console.WriteLine("\nHurry to lesson !!!");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }
        static public void EndOfWork()
        {
            Console.WriteLine("All dish, which we saled today:");            
            Statictic.PrintFile("../../../SalesStatistic.xml");
            Statictic.PrintFile("../../../Marks.xml");
        }
    }
}
