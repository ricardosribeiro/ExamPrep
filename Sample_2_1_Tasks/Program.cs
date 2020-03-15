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

            Console.WriteLine("Run a rask using AwaitAll");
            AwaitAll();

            Console.WriteLine("Continuation task sample");
            ContinuationTask();

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
        /// A Task can be created that wil return a value.
        /// </summary>
        public static void TaskReturnValue()
        {
            Task<int> task = Task.Run(() => CalculateResult());
            task.Wait();

            Console.WriteLine($"The task result is: {task.Result}");
        }

        /// <summary>
        /// The Task.AwaitAll method can be used to pause a program until a number of tasks
        /// have completed. This sample illustrates a additional issue with the use of the loop control variables.
        /// The loop counter is copied into a local variable called taskNum. If the variable i was used directly
        /// in the lambada expression, all of the tasks would have number 10.
        /// </summary>
        public static void AwaitAll()
        {
            Task[] tasks = new Task[10];

            for (int i = 0; i < 10; i++)
            {
                int taskNum = i;
                tasks[i] = Task.Run(() => DoWork(taskNum));
            }
            Task.WaitAll(tasks);
        }
        
        public static void ContinuationTaskOptions()
        {
            Task task = Task.Run(()=>One());
            task.ContinueWith()
        }

        ///<sumary>
        ///A continuation task can be nominated to start when existing task (the antecedent task) finishes.
        ///If the antecedent task produces a result, it can be supplied as an input to the continuation task.
        ///Continuation tasks can be used to create a "pipeline" of operations.
        ///</sumary>
        public static void ContinuationTask()
        {
            Task task = Task.Run(() => One())
                .ContinueWith((One)=> Two())
                .ContinueWith((Two)=>Three());
        }

        public static void DoWork()
        {
            Console.WriteLine("Work starting");
            Thread.Sleep(2000);
            Console.WriteLine($"Work finished");
        }

        public static void DoWork(int i)
        {
            Console.WriteLine($"Work {i} starting");
            Thread.Sleep(2000);
            Console.WriteLine($"Work {i} finished");

        }

        public static int CalculateResult()
        {
            Console.WriteLine("Work  starting");
            Thread.Sleep(2000);
            Console.WriteLine("Work finished");
            return 99;
        }

        public static void One()
        {
            Console.WriteLine("Hello ");
            Thread.Sleep(1000);
        }
        public static void Two()
        {
            Console.WriteLine("Cruel ");
            Thread.Sleep(1000);

        }
        public static void Three()
        {
            Console.WriteLine("World");
            Thread.Sleep(1000);
        }

    }
}
