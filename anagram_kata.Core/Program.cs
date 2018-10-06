using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Anagramalist.Implementations;

namespace anagram_kata2
{
    class Program
    {
        static void Main(string[] args)
        {
            // the data comes from here http://codekata.com/kata/kata06-anagrams/
            var expectedNumberOfAnagrams = 20683;
            var allLines = File.ReadAllLines("wordlist.txt");
            var words = allLines
                .Where(x => x != string.Empty)
                .ToArray();

            var bytes = words.Select(x => Encoding.UTF8.GetBytes(x)).ToArray();

            var sut = new AnagramalistParrallelGrouping_CustomStruct_Bytes();

            var testResult = Tester.RunMultileTests(sut, bytes, 20, expectedNumberOfAnagrams);

            Console.WriteLine($"test repeats: {20}");
            Console.WriteLine(testResult.AverageTimeSeconds);
            Console.WriteLine(testResult.MedianTimeSeconds);

            Console.WriteLine("enter to exit");
            Console.ReadLine();

            var implementations = new List<IAnagramalist>()
            {
                new AnagramalistParrallelGrouping_CustomStruct(),
                new AnagramalistLinq(),
                new AnagramalistParallelLinq(),
                new AnagramalistDictionary(),
            };
            Console.WriteLine(".Net Core");
            Tester.TestAll(words, expectedNumberOfAnagrams, implementations, testRepeatCount: 50);
        }

    }
}