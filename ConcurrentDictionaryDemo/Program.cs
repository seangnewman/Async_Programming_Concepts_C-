using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentDictionaryDemo
{
    class Program
    {
        static ConcurrentDictionary<int, string> cd = new ConcurrentDictionary<int, string>();
        static void Main(string[] args)
        {
            //if (cd.TryAdd(1, "one"))
            //{
            //    Console.WriteLine("KVP 1 was added");
            //}

            //string val = cd.GetOrAdd(1, "_ONE_");
            //Console.WriteLine("Existing 1: {0}", val);

            //string val2 = cd.AddOrUpdate(1, "ONE", (int i, string existingValue) =>
            //{
            //    return existingValue.ToUpper();
            //});

            //Console.WriteLine("Existing 1: {0}", val2);

            //if(cd.TryGetValue(1, out string val3))
            //{
            //    Console.WriteLine("Existing 1: {0}",val3);
            //}

            string filename = @"C:\Users\Sean\Source\Repos\Programming_Concepts\ConcurrentDictionaryDemo/sp_test.txt";
            string[] lines = File.ReadAllLines(filename);

            Parallel.ForEach<string>(lines, (string line) => {
                string[] words = line.Split(' ');
                foreach(string word in words)
                {
                    if(String.IsNullOrWhiteSpace(word))
                    {
                        continue;
                    }

                    wordCount.AddOrUpdate(word, 1, (k, currentCount) =>
                    {
                        return currentCount + 1;
                    });
                }
            });


        }
       static  ConcurrentDictionary<string, uint> wordCount = new ConcurrentDictionary<string, uint>();
    }
}
