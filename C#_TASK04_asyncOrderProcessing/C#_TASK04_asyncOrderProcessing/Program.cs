using System;
using System.Threading.Tasks; 

namespace AsyncOrderProcessing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("===== Online Order Processing =====");

            
            Console.Write("Enter the name of your order: ");
            string orderName = Console.ReadLine();

            
            Console.WriteLine("\nProcessing order...");

            
            await ProcessOrderAsync(orderName);

            Console.WriteLine($"Order for {orderName} is ready!");
        }

       
        static async Task ProcessOrderAsync(string order)
        {
            
            await Task.Delay(3000); 
        }
    }
}
