using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* Write a program that takes a list of integers from the user and performs the following operations using LINQ:
a) Find all numbers greater than 50
b) Sort the numbers in ascending order
c) Find the square of each number
*/

namespace C__TASK02_LINQDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a list of integers separated by spaces:");
            string input = Console.ReadLine();

            List<int> numbers = input.Split(' ')
                .Select(int.Parse)
                .ToList();

            var greaterThan50 = numbers.Where(n => n > 50).ToList();
            Console.WriteLine("\n Numbers greater than 50:");
            foreach(var num in greaterThan50)
            {
                Console.WriteLine(num);
            }

            var sortedNumbers = numbers.OrderBy(n => n).ToList();
            Console.WriteLine("\n Numbers in ascending order:");
            foreach (var item in sortedNumbers)
            {
                Console.WriteLine(item);
            }

            var squares = numbers.Select(n => n*n).ToList();
            Console.WriteLine("\n Squares of each number:");
            foreach(var square in squares)
            {
                Console.WriteLine(square);
            }
            Console.ReadKey();
        }
    }
}
