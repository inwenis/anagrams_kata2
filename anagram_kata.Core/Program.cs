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
            Console.WriteLine(".Net Core");

            // the data comes from here http://codekata.com/kata/kata06-anagrams/
            var expectedNumberOfAnagrams = 20683;
            var allBytes = File.ReadAllBytes("wordlist.txt");

            var testRepeatCount = 50;

            var implementations = new List<IAnagramalist>()
            {
                new AnagramalistParrallelGrouping_CustomStruct_Bytes(),
                new AnagramalistParrallelGrouping_CustomStruct_Bytes_2(),
                new AnagramalistParrallelGrouping_CustomStruct(),
                new AnagramalistParallelLinq(),
            };
            Tester.TestAll(allBytes, expectedNumberOfAnagrams, implementations, testRepeatCount: testRepeatCount);
        }

    }
}