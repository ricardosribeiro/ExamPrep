using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample_1_4_Managing_Parallel_For
{
    class Programm
    {
        static void Main(string[] args)
        {
            ManagingParallelFor();
        }

        /// <summary>
        /// The lambda expression that executes each iteration of the loop can be
        /// provided with and additional parameter of type ParallelLoopState that
        /// allows the code being iterated to control the iteration process.
        /// The For and ForEach methods also return a value of type ParallelLoopResult
        /// that can be used to determine whether or not a parallel loop has
        /// successfully completed.
        /// </summary>
        private static void ManagingParallelFor()
        {
            var items = Enumerable.Range(0, 500).ToArray();

            var result = Parallel.For(0, items.Length, (int i, ParallelLoopState loopState) =>
             {
                 if (i == 200) loopState.Stop();
                 WorkOnItem(items[i]);
             });

            Console.WriteLine("Completed: " + result.IsCompleted);
            Console.WriteLine("Items: " + result.LowestBreakIteration);
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
