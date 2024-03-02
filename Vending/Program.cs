using System;
using System.Text;

namespace Vending_Machine
{
    class Program
    {
        static float number()
        {
            float input;
            try
            {
                Console.WriteLine("Insert Coin: ");
                input = float.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: only numbers 1, 5, 10, and 25 allowed");
                return -1;
            }
            return input;
        }

        static void Main(string[] args)
        {
            double due = 50;
            while (due > 0)
            {
                Console.WriteLine("Amount Due: " + due);
                float num = number();

                if (num == 1 || num == 5 || num == 10 || num == 25)
                {
                    due = due - num;
                }
                else if ((num != 1 && num != 5 && num != 10 && num != 25))
                {
                    Console.WriteLine($"Error: only numbers 1, 5, 10, and 25 allowed.");
                }
               
            }
            
            if (due < 0)
            {
                due = Math.Abs(due);
                Console.WriteLine("Change Due: ", due);
                Console.WriteLine(due);
            }
            else if (due == 0)
            {
                Console.WriteLine("Change Due: ", due);
                Console.WriteLine(due);
            }
        }
    }
}

