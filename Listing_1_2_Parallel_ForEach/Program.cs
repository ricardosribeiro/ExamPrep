using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample_1_2_Parallel_ForEach
{
    class Program
    {
        static void Main(string[] args)
        {
            UsingParallelForEach();
        }

        /// <summary>
        /// The Parallel class also provides a ForEach method that perform a parallel implementation of ForEach 
        /// loop contruction.
        /// </summary>
        private static void UsingParallelForEach()
        {
            var items = Enumerable.Range(0, 500);
            Parallel.ForEach(items, item =>
            {
                WorkOnItem(item);
            });

            Console.WriteLine("Finished processing. Press any key to end");
            Console.ReadKey();
        }

        private static void WorkOnItem(object item)
        {
            Console.WriteLine("Started working on: "+item);
            Thread.Sleep(100);
            Console.WriteLine("Finished working on: " + item);
        }
    }
}
