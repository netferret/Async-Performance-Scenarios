using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProject
{
    public class AsyncTests
    {

        public async Task<(int, long)> TestStringAsync()
        {
            Console.WriteLine("Task on thread {0} started.", Thread.CurrentThread.ManagedThreadId);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var dt = new Random();
            var result = dt.Next(1,10);

            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://swapi.co/api/people/" + result.ToString());

                if (response.Result.IsSuccessStatusCode)
                {
                    stopwatch.Stop();
                    return (result, stopwatch.ElapsedMilliseconds);
                }
                else { throw new Exception(); }
            }
            
        }

        public (int, long) TestStringSync()
        {
            Console.WriteLine("Task on thread {0} started.",  Thread.CurrentThread.ManagedThreadId);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var dt = new Random();
            var result = dt.Next(1, 10);

            using (var client = new HttpClient())
            {
                // Ok this uses async, but it should wait for the result.
                var response = client.GetAsync("https://swapi.co/api/people/" + result.ToString());

                if (response.Result.IsSuccessStatusCode)
                {
                    stopwatch.Stop();
                    return (result, stopwatch.ElapsedMilliseconds);
                }
                else { throw new Exception(); }
            }
        }
    }
}
