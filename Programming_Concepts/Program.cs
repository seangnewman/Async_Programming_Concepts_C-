using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Concepts
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintSum(1, 2, 3);
            PrintSum(10, 20, 30);
            PrintSum(100, 200, 300);
        }

        static void PrintSum(int a, int b, int c)
        {
            Console.WriteLine("Sum: {0}", a + b + c);
        }
    }
}
