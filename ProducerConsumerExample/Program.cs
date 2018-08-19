using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerExample
{
    

    class Program
    {
        static Random rnd = new Random();
       // static ConcurrentQueue<ulong> cq = new ConcurrentQueue<ulong>();

        static BlockingCollection<ulong> numbers = new BlockingCollection<ulong>(10);
        static void Main(string[] args)
        {
            Thread threadFib = new Thread(new ThreadStart(GenerateFib));
            threadFib.IsBackground = false;
            threadFib.Start();

            Thread threadReader = new Thread(new ThreadStart(ReadFib));
            threadReader.IsBackground = false;
            threadReader.Start();
        }
        private static void ReadFib()
        {
            Thread.Sleep(10000);
            Console.WriteLine("Starting to read from the queue....");
            


            do
            {
                var n = numbers.Take();
                //    if (cq.TryDequeue(out ulong n))
                //    {
                        Console.Write("[Fib {0}] ", n);
                //    }
                //    else
                //    {
                //        //Console.Write(".");
                //    }
                //    Thread.Sleep(10);

            } while (true);
        }

        private static void GenerateFib()
        {
            for (ushort ix = 0; ix < 50; ix++)
            {
                Thread.Sleep(rnd.Next(1, 500));
                //ulong fibN = Fibonacci(ix);
                //cq.Enqueue(fibN);
                numbers.Add(Fibonacci(ix));
                Console.WriteLine("Adding next Fib...");
            }
        }

        //Fibonacci Sequence
        private static ulong Fibonacci(ushort n)
        {
            return (n == 0) ? 0 : Fib(n);
        }

        private static ulong Fib(ushort n)
        {
            ulong a = 0;
            ulong b = 1;

            for (uint ix = 0; ix < n - 1; ix++)
            {
                ulong next = checked(a + b);
                a = b;
                b = next;
            }

            return b;
        }


    }
}
