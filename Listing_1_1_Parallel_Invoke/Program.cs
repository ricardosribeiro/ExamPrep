using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample_1_1_Parallel_Invoke
{
    class Program
    {
        static void Main(string[] args)
        {
            ParallelInvokeMethod();
        }

        /// <summary>
        /// Parallel.Invoke can start a large number of tasks at one and returns when all of the tasks have 
        /// completed. You have no control over te order in which the tasks are started or which processor they are 
        /// assigned to. 
        /// </summary>
        private static void ParallelInvokeMethod()
        {
            Parallel.Invoke(() => Task1(), () => Task2());

            Console.WriteLine("Finished processing. Press any key to end");
            Console.ReadKey();
        }

        private static void Task1()
        {
            Console.WriteLine("Task 1 starting");
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 ending");
        }
        private static void Task2()
        {
            Console.WriteLine("Task 2 starting");
            Thread.Sleep(2000);
            Console.WriteLine("Task 2 ending");
        }
    }
}
