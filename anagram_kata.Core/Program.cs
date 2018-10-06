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
            var allLines = File.ReadAllLines("wordlist.txt");
            var words = allLines
                .Where(x => x != string.Empty)
                .ToArray();

            var bytes = words.Select(x => Encoding.UTF8.GetBytes(x)).ToArray();

            var testRepeatCount = 50;
            var implementationsUsingBytes = new List<IAnagramalistFromBytes>()
            {
                new AnagramalistParrallelGrouping_CustomStruct_Bytes(),
                new AnagramalistParrallelGrouping_CustomStruct_Bytes_2(),
            };
            Tester_bytes.TestAll(bytes, expectedNumberOfAnagrams, implementationsUsingBytes, testRepeatCount: testRepeatCount);

            var implementations = new List<IAnagramalist>()
            {
                new AnagramalistParrallelGrouping_CustomStruct(),
                new AnagramalistParallelLinq(),
            };
            Tester.TestAll(words, expectedNumberOfAnagrams, implementations, testRepeatCount: testRepeatCount);
        }

    }
}