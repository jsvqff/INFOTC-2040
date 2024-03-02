using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            int amountDue = 50;
            int userCoin = 0;
            List<int> coins = new List<int>(){1, 5, 10, 25};
            Console.WriteLine("Vending Machine\n----------------");
            
            while(true){
                Console.WriteLine($"Amount Due: {amountDue}");
                Console.WriteLine("Insert Coin: ");
                try{
                    userCoin = int.Parse(Console.ReadLine());
                    if(!coins.Contains(userCoin)){
                        continue;
                    }
                }catch(Exception){
                    continue;
                }

                amountDue -= userCoin;
                if(amountDue <= 0){
                    break;
                }
            }
            
            Console.WriteLine($"Change Owed: {Math.Abs(amountDue)}");
        }
    }
}
