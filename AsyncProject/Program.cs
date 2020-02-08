﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var asyncTests = new AsyncTests();

            var result = Task.Run(async () => await asyncTests.TestStringAsync());
            var result2 = Task.Run(async () => await asyncTests.TestStringAsync());
            var result3 = Task.Run(async () => await asyncTests.TestStringAsync());
            var result4 = Task.Run(async () => await asyncTests.TestStringAsync());

            Task.WaitAll(result, result2, result3, result4);
            
            Console.WriteLine($"Task 1 - Passed {result.Result.Item1} - Took {result.Result.Item2} ms ");
            Console.WriteLine($"Task 2 - Passed {result2.Result.Item1} - Took {result2.Result.Item2} ms ");
            Console.WriteLine($"Task 3 - Passed {result3.Result.Item1} - Took {result3.Result.Item2} ms ");
            Console.WriteLine($"Task 4 - Passed {result4.Result.Item1} - Took {result4.Result.Item2} ms ");
            stopwatch.Stop();
            Console.WriteLine($"ASYNC - Completed in {stopwatch.ElapsedMilliseconds} ms" + Environment.NewLine);






            stopwatch.Reset();
            stopwatch.Start();

            var syncResult = asyncTests.TestStringSync();
            Console.WriteLine($"Task 1 - Passed {syncResult.Item1} - Took {syncResult.Item2} ms ");
            var syncResult2 = asyncTests.TestStringSync();
            Console.WriteLine($"Task 2 - Passed {syncResult2.Item1} - Took {syncResult2.Item2} ms ");
            var syncResult3 = asyncTests.TestStringSync();
            Console.WriteLine($"Task 3 - Passed {syncResult3.Item1} - Took {syncResult3.Item2} ms ");
            var syncResult4 = asyncTests.TestStringSync();
            Console.WriteLine($"Task 4 - Passed {syncResult4.Item1} - Took {syncResult4.Item2} ms ");

            stopwatch.Stop();
            Console.WriteLine($"SYNC - Completed in {stopwatch.ElapsedMilliseconds} ms" + Environment.NewLine);


            stopwatch.Reset();
            stopwatch.Start();

            var syncTaskResult = Task.Run(() => asyncTests.TestStringSync());
            var syncTaskResult2 = Task.Run(() => asyncTests.TestStringSync());
            var syncTaskResult3 = Task.Run(() => asyncTests.TestStringSync());
            var syncTaskResult4 = Task.Run(() => asyncTests.TestStringSync());

            Task.WhenAll(syncTaskResult, syncTaskResult2, syncTaskResult2, syncTaskResult2);

            Console.WriteLine($"Task 1 - Passed {syncTaskResult.Result.Item1} - Took {syncTaskResult.Result.Item2} ms ");
            Console.WriteLine($"Task 2 - Passed {syncTaskResult2.Result.Item1} - Took {syncTaskResult2.Result.Item2} ms ");
            Console.WriteLine($"Task 3 - Passed {syncTaskResult3.Result.Item1} - Took {syncTaskResult3.Result.Item2} ms ");
            Console.WriteLine($"Task 4 - Passed {syncTaskResult4.Result.Item1} - Took {syncTaskResult4.Result.Item2} ms ");

            stopwatch.Stop();
            Console.WriteLine($"SYNC with Tasks - Completed in {stopwatch.ElapsedMilliseconds} ms");
            Console.ReadKey();

        }
    }
}