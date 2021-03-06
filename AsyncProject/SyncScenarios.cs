﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;

namespace AsyncProject
{
    public sealed class SyncScenarios : IDisposable
    {
        public void Dispose()
        {
        }

        public (bool, long) TestStringSync()
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
                // Ok this uses async, but it should wait for the result.
                var response = client.GetAsync("https://swapi.co/api/people/" + result.ToString());

                if (response.Result.IsSuccessStatusCode)
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
