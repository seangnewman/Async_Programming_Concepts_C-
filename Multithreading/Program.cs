using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting in main method");
            var t = new Thread(new ThreadStart(DoWork));
            t.Start();
            Thread.Sleep(1000);
            Console.WriteLine("Ending in main method");
        }

        private static void DoWork()
        {
            Console.WriteLine("Work to Do!");
            Thread.Sleep(1000);
        }
    }
}
