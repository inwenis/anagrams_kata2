using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Anagramalist.Implementations
{
    public class Tester
    {
        public static void TestAll(byte[] words, int expectedNumberOfAnagrams, List<IAnagramalist> suts, int testRepeatCount)
        {
            var results = new List<dynamic>();
            foreach (var sut in suts)
            {
                var name = sut.GetType().Name;
                Console.WriteLine($"{name}");
                var testResult = RunMultileTests(sut, words, testRepeatCount, expectedNumberOfAnagrams);
                results.Add(new {name, testResult});
            }

            int index = 1;
            foreach (var name_result in results.OrderBy(x => x.testResult.MedianTimeSeconds))
            {
                Console.WriteLine(
                    $"{index}. {name_result.name,-51} median from {testRepeatCount} tests: {name_result.testResult.MedianTimeSeconds:f6}s");
                index++;
            }
        }

        public static TestResult RunMultileTests(IAnagramalist sut, byte[] words, int testRepeatCount, int expectedNumberOfAnagrams)
        {
            // run the anagramalist here so the JITer will compile all code
            var resultLost = RunSingleTest(sut, words);

            double sumSeconds = 0;
            var results = new List<SingleTestResult>();
            for (int i = 1; i <= testRepeatCount; i++)
            {
                var result = RunSingleTest(sut, words);
                results.Add(result);
                sumSeconds += result.Time.TotalSeconds;
                if (result.Anagrams.Length != expectedNumberOfAnagrams)
                {
                    Console.WriteLine("Wrong Number of anagrams!");
                    Console.WriteLine(result.Anagrams.Length);
                }

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(
                    $"test {i} of {testRepeatCount}    last run: {result.Time.TotalSeconds}s average: {sumSeconds / (i)}s          ");
            }

            Console.WriteLine();

            var testResult = TestResult.Create(results);

            return testResult;
        }

        private static SingleTestResult RunSingleTest(IAnagramalist sut, byte[] words)
        {
            var sw = new Stopwatch();
            sw.Start();
            var allAnagrams = sut.FindAllAnagrams(words);
            sw.Stop();
            return new SingleTestResult
            {
                Anagrams = allAnagrams,
                Time = sw.Elapsed
            };
        }

        public class SingleTestResult
        {
            public string[] Anagrams { get; set; }
            public TimeSpan Time { get; set; }
        }

        public class TestResult
        {
            public List<SingleTestResult> Results;
            public double AverageTimeSeconds;
            public double MedianTimeSeconds;

            public static TestResult Create(List<SingleTestResult> results)
            {
                var testResult = new TestResult();
                testResult.Results = results;
                testResult.AverageTimeSeconds = results.Sum(x => x.Time.TotalSeconds) / results.Count;
                testResult.MedianTimeSeconds = Median(results.Select(x => x.Time));
                return testResult;
            }

            private static double Median(IEnumerable<TimeSpan> values)
            {
                var array = values.ToArray();
                if (array.Length % 2 == 0)
                {
                    var left = array[(array.Length - 2) / 2];
                    var right = array[array.Length / 2];
                    return (left + right).TotalSeconds / 2;
                }
                else
                {
                    return array[array.Length / 2].TotalSeconds;
                }
            }
        }
    }
}
