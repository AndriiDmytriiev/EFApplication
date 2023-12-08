using Task = System.Threading.Tasks.Task;
using Thread = System.Threading.Thread;
using Barrier = System.Threading.Barrier;
using Monitor = System.Threading.Monitor;
using IDisposable = System.IDisposable;
using TaskEnum = System.Collections.Generic.IEnumerable<System.Threading.Tasks.Task>;
using TaskQueue = System.Collections.Generic.Queue<System.Threading.Tasks.Task>;
using Enumerable = System.Linq.Enumerable;
using ObjectDisposedException = System.ObjectDisposedException;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using _Imported_Extensions_;
using System.Data;
using System.Globalization;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace _Imported_Extensions_
{

    public static class Extensions
    {
        public static bool Any(this TaskEnum te)
        {
            return Enumerable.Any(te);
        }

        public static TaskEnum ToList(this TaskEnum te)
        {
            return Enumerable.ToList(te);
        }
    }
}

namespace EFApplication
{
    
    internal class Program
    {
        public static string strDateString = "";
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            // Create a scheduler that uses two threads. 

            List<Task> tasks = new List<Task>();

            // Create a TaskFactory and pass it our custom scheduler. 


            // Use our factory to run a set of tasks. 
            Object lockObj = new Object();
            int outputItem = 0;

            for (int tCtr = 0; tCtr <= 1; tCtr++)
            {
                int iteration = tCtr;
                Task t = Task.Run(() => {
                    for (int i = 1; i < 5000; i++)
                    {
                        lock (lockObj)
                        {
                            doStuff("Task" + i.ToString(), i);

                            outputItem++;

                        }
                    }
                });



                tasks.Add(t);
            }


            ExecuteJob(tasks.ToArray());

            Console.WriteLine("Work of programm within: {0:f2} s", sw.Elapsed.TotalSeconds);
            Console.ReadKey();
        }
        private static async void ExecuteJob(Task[] test)
        {

            await Task.WhenAll(test).ConfigureAwait(false);
        }
        public static void doStuff(string strName, int j)
        {

            using (var dbContext = new AppDbContext())
            {
                // Add a new person
                dbContext.people.Add(new Person { id = j, firstname = "John", lastname = "Doe" });
               
                dbContext.SaveChanges();

                // Query all people
                var people = dbContext.people.ToList();

                
            }

            




        }
    }
}
