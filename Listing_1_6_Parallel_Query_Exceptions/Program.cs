using System;
using System.Linq;

namespace Sample_1_6_Parallel_Query_Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[]
            {
                new Person("Alan", "Hull"),
                new Person("Beryl", ""),
                new Person("Charles", "London"),
                new Person("David", "Seattle"),
                new Person("Eddy", ""),
                new Person("Fred", "Berlin"),
                new Person("Gordon", "Hull"),
                new Person("Henry", ""),
                new Person("Isaac", "Seattle"),
                new Person("James", "London")
            };

            Console.WriteLine("Using Agreggate Exception in PLINQ query");

            ParallelQueryWithExceptions(persons);

            Console.WriteLine("Finished processing. Press any key to end");
            Console.ReadKey();
        }

        /// <summary>
        /// The AgregateException does catch any exception thrown by the CheckCity method.
        /// If elements of a query can be generate exceptions it is considered good programing practice 
        /// to catch and deal with them as close to the source as possible.
        /// </summary>
        
        public static void ParallelQueryWithExceptions(Person[] persons)
        {
            try
            {
                var result = from p in persons.AsParallel()
                             where CheckCity(p.City)
                             select p;

                result.ForAll(item => Console.WriteLine($"Name: {item.Name}, City: {item.City}"));

            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.InnerExceptions.Count+" exceptions.");
                throw;
            }
        }

       /// <summary>
       /// This CheckCitty method throws an exception when the city name is empty. 
       /// Using this method in a PLINQ query will cause exceptions to be thorwn when empty city names are encountered in the data.
       /// </summary>
       /// <returns></returns>
        public static bool CheckCity(string name)
        {
            if (name == "")
                throw new ArgumentException(name);
            return name == "Seattle";
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
