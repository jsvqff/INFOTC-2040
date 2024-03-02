using System;
using System.Collections.Generic;

namespace MenuSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, float> menuItems = new Dictionary<string, float>(){
                {"Mega Taco", 4.00f},
                {"Burrito", 7.50f},
                {"Bowl", 8.50f},
                {"Nachos", 11.00f},
                {"Quesadilla", 8.50f},
                {"Super Burrito", 8.50f},
                {"Super Quesadilla", 9.50f},
                {"Taco", 3.00f},
                {"Tortilla Salad", 8.00f}
            };
            float orderTotal = 0;
            string itemOrder;

            while(true){
                Console.WriteLine("\nItem: ");
                itemOrder = Console.ReadLine();
                
                if(itemOrder.ToLower() == "end"){
                    break;
                }

                if(menuItems.ContainsKey(itemOrder)){
                    orderTotal += menuItems[itemOrder];
                }else{
                    continue;
                }
                Console.WriteLine($"Total: ${orderTotal:N2}");
            }
        }
    }
}
