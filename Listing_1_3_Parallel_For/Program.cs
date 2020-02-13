using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample_1_3_Parallel_For
{
    class Program
    {
        static void Main(string[] args)
        {
            UsingParallelFor();
        }

        /// <summary>
        /// The Parallel.For can be used to parallelize the execution of a For loop. 
        /// </summary>
        private static void UsingParallelFor()
        {
            var items = Enumerable.Range(0, 500).ToArray();

            Parallel.For(0, items.Length, i =>
            {
                WorkOnItem(items[i]);
            });

            Console.WriteLine("Finished processing. Press any key to end");
            Console.ReadKey();

        }
        private static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: " + item);
            Thread.Sleep(100);
            Console.WriteLine("Finished working on: " + item);
        }
    }
}
