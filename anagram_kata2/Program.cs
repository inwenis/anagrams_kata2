using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var implementations = new List<IAnagramalist>()
            {
                new AnagramalistParrallelGrouping_CustomStruct(),
                new AnagramalistLinq(),
                new AnagramalistParallelLinq(),
                new AnagramalistDictionary(),
            };
            Console.WriteLine(".Net Framework");
            Tester.TestAll(words, expectedNumberOfAnagrams, implementations, testRepeatCount: 50);
        }
    }
}