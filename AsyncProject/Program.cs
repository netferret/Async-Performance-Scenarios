using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace AsyncProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var process = Process.GetCurrentProcess().Threads.AsParallel();
            var stopwatch = new Stopwatch();

            using (var asyncScenarios = new AsyncScenarios())
            {
                stopwatch.Start();
                var result = Task.Run(async () => await asyncScenarios.TestStringAsync());
                var result2 = Task.Run(async () => await asyncScenarios.TestStringAsync());
                var result3 = Task.Run(async () => await asyncScenarios.TestStringAsync());
                var result4 = Task.Run(async () => await asyncScenarios.TestStringAsync());

                Task.WaitAll(result, result2, result3, result4);

                WriteLine($"Task 1 - Passed? {result.Result.Item1} - Took {result.Result.Item2} ms ");
                WriteLine($"Task 2 - Passed? {result2.Result.Item1} - Took {result2.Result.Item2} ms ");
                WriteLine($"Task 3 - Passed? {result3.Result.Item1} - Took {result3.Result.Item2} ms ");
                WriteLine($"Task 4 - Passed? {result4.Result.Item1} - Took {result4.Result.Item2} ms ");
                stopwatch.Stop();
                WriteLine($"ASYNC with Tasks - Completed in {stopwatch.ElapsedMilliseconds} ms" + Environment.NewLine);
            }

            using (var asyncScenarios = new AsyncScenarios())
            {
                stopwatch.Reset();
                stopwatch.Start();
                var result = await asyncScenarios.TestStringAsync();
                var result2 = await asyncScenarios.TestStringAsync();
                var result3 = await asyncScenarios.TestStringAsync();
                var result4 = await asyncScenarios.TestStringAsync();

                //Task.WaitAll(result, result2, result3, result4);

                WriteLine($"Task 1 - Passed? {result.Item1} - Took {result.Item2} ms ");
                WriteLine($"Task 2 - Passed? {result2.Item1} - Took {result2.Item2} ms ");
                WriteLine($"Task 3 - Passed? {result3.Item1} - Took {result3.Item2} ms ");
                WriteLine($"Task 4 - Passed? {result4.Item1} - Took {result4.Item2} ms ");
                stopwatch.Stop();
                WriteLine($"ASYNC without Tasks - Completed in {stopwatch.ElapsedMilliseconds} ms" + Environment.NewLine);
            }


            using (var syncScenarios = new SyncScenarios())
            {
                stopwatch.Reset();
                stopwatch.Start();

                var syncResult = syncScenarios.TestStringSync();
                WriteLine($"Task 1 - Passed? {syncResult.Item1} - Took {syncResult.Item2} ms ");
                var syncResult2 = syncScenarios.TestStringSync();
                WriteLine($"Task 2 - Passed? {syncResult2.Item1} - Took {syncResult2.Item2} ms ");
                var syncResult3 = syncScenarios.TestStringSync();
                WriteLine($"Task 3 - Passed? {syncResult3.Item1} - Took {syncResult3.Item2} ms ");
                var syncResult4 = syncScenarios.TestStringSync();
                WriteLine($"Task 4 - Passed? {syncResult4.Item1} - Took {syncResult4.Item2} ms ");

                stopwatch.Stop();
                WriteLine($"SYNC without Tasks - Completed in {stopwatch.ElapsedMilliseconds} ms" + Environment.NewLine);
            }

            using (var syncScenarios = new SyncScenarios())
            {
                stopwatch.Reset();
                stopwatch.Start();

                var syncTaskResult = Task.Run(() => syncScenarios.TestStringSync());
                var syncTaskResult2 = Task.Run(() => syncScenarios.TestStringSync());
                var syncTaskResult3 = Task.Run(() => syncScenarios.TestStringSync());
                var syncTaskResult4 = Task.Run(() => syncScenarios.TestStringSync());

                await Task.WhenAll(syncTaskResult, syncTaskResult2, syncTaskResult2, syncTaskResult2);

                WriteLine($"Task 1 - Passed? {syncTaskResult.Result.Item1} - Took {syncTaskResult.Result.Item2} ms ");
                WriteLine($"Task 2 - Passed? {syncTaskResult2.Result.Item1} - Took {syncTaskResult2.Result.Item2} ms ");
                WriteLine($"Task 3 - Passed? {syncTaskResult3.Result.Item1} - Took {syncTaskResult3.Result.Item2} ms ");
                WriteLine($"Task 4 - Passed? {syncTaskResult4.Result.Item1} - Took {syncTaskResult4.Result.Item2} ms ");

                stopwatch.Stop();
                WriteLine($"SYNC with Tasks - Completed in {stopwatch.ElapsedMilliseconds} ms");
            }
            ReadKey();

        }
    }
}
