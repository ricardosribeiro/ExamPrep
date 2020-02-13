using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample_2_1_Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run a task using a single method");
            CreateTaskUsingASingleMethod();

            Console.WriteLine("Return a value from a task");
            TaskReturnValue();

            Console.WriteLine("Finished processing. Press any key to end");
            Console.ReadKey();
        }

        /// <summary>
        /// The CreateTask method creates a task, starts ir running, and waits for the task to complete.
        /// </summary>
        public static void CreateTask()
        {
            Task task = new Task(() => DoWork());            
            task.Start();
            task.Wait();
        }

        /// <summary>
        /// Task.Run() method can be used to start the task and them run to completion.
        /// </summary>
        public static void CreateTaskUsingASingleMethod()
        {
            Task task = Task.Run(() => DoWork());
            task.Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        public static void TaskReturnValue()
        {
            Task<int> task = Task.Run(() => CalculateResult());
            task.Wait();

            Console.WriteLine($"The task result is: {task.Result}");
        }

        public static void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine("Wotk finished");
        }

        public static int CalculateResult()
        {
            Console.WriteLine("Work  starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
            return 99;
        }
    }
}
