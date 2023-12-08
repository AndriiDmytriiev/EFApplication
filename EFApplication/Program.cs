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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

namespace EFApplication
{
    
    internal class Program(IConfiguration config)
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
                    for (int i = 1; i < 5; i++)
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
                //Adding data to the table "accounts"
                using (var dbContext = new AppDbContext())
                {
                    // Add a new account
                    dbContext.accounts.Add(new Account { Id = j, AccountNumber = "1111111" + j, Balance = 2*j++ });

                    dbContext.SaveChanges();

                    // Query all accounts
                    var account = dbContext.accounts.ToList();


                }
                using (var dbContext = new AppDbContext())
            {
                // Replace with the actual account IDs and amount
                int fromAccountId = j;
                int toAccountId = j++;
                decimal transferAmount = j;

                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // Execute the stored procedure
                        dbContext.Database.ExecuteSqlAsync($"call testdbthread.public.transfer({j},{fromAccountId},{transferAmount},{j*2});");

                        // Commit the transaction if everything is successful
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction in case of an exception
                        //transaction.Rollback();
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }





            }
        }
    }
}
