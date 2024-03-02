using System;
using System.Collections.Generic;

namespace MenuOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, double> menu = new Dictionary<string, double>
            {
                { "Baja Taco", 4.00 },
                { "Burrito", 7.50 },
                { "Bowl", 8.50 },
                { "Nachos", 11.00 },
                { "Quesadilla", 8.50 },
                { "Super Burrito", 8.50 },
                { "Super Quesadilla", 9.50 },
                { "Taco", 3.00 },
                { "Tortilla Salad", 8.00 }
            };

            double totalCost = 0.00;

            Console.WriteLine("Welcome to the restaurant!");
            Console.WriteLine("Please enter your order items one per line. Type 'end' to finish the order.");

            while (true)
            {
                Console.Write("Enter an item: ");
                string input = Console.ReadLine().Trim();

                string formattedInput = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);

                if (formattedInput.Equals("End", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (menu.ContainsKey(formattedInput))
                {
                    double itemPrice = menu[formattedInput];
                    totalCost += itemPrice;
                    Console.WriteLine($"Added {formattedInput} - ${itemPrice:F2}");
                }
                else
                {
                    Console.WriteLine("Invalid item. Please enter a valid menu item.");
                }
            }

            Console.WriteLine($"Your total order cost is: ${totalCost:F2}");
            Console.WriteLine("Thank you for your order!");
        }
    }
}

