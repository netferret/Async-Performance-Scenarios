using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProject
{
    public sealed class AsyncScenarios : IDisposable
    {
        public void Dispose()
        {
        }

        public async Task<(bool, long)> TestStringAsync()
        {
            Console.WriteLine("Task on thread {0}", Thread.CurrentThread.ManagedThreadId);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Randomise Result 
            var dt = new Random();
            var result = dt.Next(1, 10);

            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, 5);
                var response = await client.GetAsync("https://swapi.co/api/people/" + result.ToString());

                if (response.IsSuccessStatusCode)
                {
                    stopwatch.Stop();
                    return (true, stopwatch.ElapsedMilliseconds);
                }
                else
                {
                    stopwatch.Stop();
                    return (false, stopwatch.ElapsedMilliseconds);
                }
            }

        }

    }
}
