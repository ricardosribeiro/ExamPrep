using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_1_5_Parallel_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[]
            {
                new Person("Alan", "Hull"),
                new Person("Beryl", "Seattle"),
                new Person("Charles", "London"),
                new Person("David", "Seattle"),
                new Person("Eddy", "Paris"),
                new Person("Fred", "Berlin"),
                new Person("Gordon", "Hull"),
                new Person("Henry", "Seattle"),
                new Person("Isaac", "Seattle"),
                new Person("James", "London")
            };

            Console.WriteLine("Parallel Query");
            ParallelQuery(persons);

            Console.WriteLine("Using ForAll loop");
            ForAllLoop(persons);

            Console.WriteLine("Parallel query with degree of parallelism options and force parallelism");
            ParallelQueryOptions(persons);

            Console.WriteLine("Ordered Parallel Query");
            OrderedParallelQuery(persons);

            Console.WriteLine("Turn off Ordered Parallel Query to improve performance");
            UnorderedParallelQuery(persons);

            Console.WriteLine("Sequential Query");
            SequentialParallelQuery(persons);



            Console.WriteLine("Finished processing. Press any key to end");
            Console.ReadKey();
        }

        

        private static void ParallelQuery(Person[] persons)
        {
            var result = from p in persons.AsParallel()
                         where p.City == "Seattle"
                         select p;

            result.ForAll(item => Console.WriteLine($"Name: {item.Name}, City: {item.City}"));
        }

        /// <summary>
        /// The WithDegreeOfParallelism method defines a maximum number os processors to execute the query.
        /// The WithExecutionMode method can be used with ParallelExecutionMode enum to force parallelism
        /// </summary>
        /// <param name="persons"></param>
        private static void ParallelQueryOptions(Person[] persons)
        {
            var result = from p in persons
                         .AsParallel()
                         .WithDegreeOfParallelism(4)
                         .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                         where p.City == "Seattle"
                         select p;

            result.ForAll(item => Console.WriteLine($"Name: {item.Name}, City: {item.City}"));
        }

        /// <summary>
        /// The AsOrdered method doesn't prevent de parallelization of the query, instead it
        /// organizes the output so that it is the same order as the original
        /// data. This can slow down the query.
        /// </summary>
        /// <param name="persons"></param>
        private static void OrderedParallelQuery(Person[] persons)
        {
            var result = from p in persons
                         .AsParallel()
                         .AsOrdered()
                         where p.City == "Seattle"
                         select p;         

            foreach (var item in result)
                Console.WriteLine($"Name: {item.Name}, City: {item.City}");
        }

        /// <summary>
        /// The AsOrdered method turn off the LINQ engine to the default behavior.
        /// </summary>
        /// <param name="persons"></param>
        private static void UnorderedParallelQuery(Person[] persons)
        {
            //Using as Ordered to ensure that Take method returns the first 4 persons IN THE SOURCE SEQUENCE that meet the condition
            var result1 = (from p in persons
                          .AsParallel()
                          .AsOrdered()
                          where p.City == "Seattle"
                          select p).Take(4);

            //Using AsUnordered method to turn it of order preservation
            var result2 = from p in result1
                          .AsParallel()
                          .AsUnordered()
                          where p.Name.Contains("d")
                          select p;

            result2.ForAll(item => Console.WriteLine($"Name: {item.Name}, City: {item.City}"));


        }

        /// <summary>
        /// The ForAll method can be used to iterate through all of the elements in a query. It differs from the foreach C# contruction
        /// in that the iteration takes place in parallel and will start before the query is complete.
        /// </summary>
        private static void ForAllLoop(Person[] persons)
        {
            var result = from p in persons
                         .AsParallel()
                         where p.Name == "Isaac"
                         select p;

            result.ForAll(item => Console.WriteLine($"Name: {item.Name}, City: {item.City}"));
        }

        private static void SequentialParallelQuery(Person[] persons)
        {
            var result = (from p in persons
                         .AsParallel()
                          where p.City == "Seattle"
                          select p).AsSequential().Take(4);

            foreach (var item in result)
                Console.WriteLine($"Name: {item.Name}, City: {item.City}");
        }

    }
    public class Person
    {
        public Person(string name, string city)
        {
            Name = name;
            City = city;
        }
        public string Name { get; private set; }
        public string City { get; private set; }
    }
}
