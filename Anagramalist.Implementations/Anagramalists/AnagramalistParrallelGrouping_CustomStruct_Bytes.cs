using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using test_string_vs_struct;

namespace Anagramalist.Implementations
{
    public class AnagramalistParrallelGrouping_CustomStruct_Bytes : IAnagramalist
    {
        public string[] FindAllAnagrams(byte[] bytes)
        {
            var splitByteArray = SplitByteArray(bytes, Encoding.UTF8.GetBytes("\n").Single());

            var anagrams = splitByteArray
                .AsParallel()
                .GroupBy(word => IRepresentOrderdString.FromBytes(word))
                .Where(group => group.Count() > 1)
                .Select(group => string.Join(" ", group.Select(x => Encoding.UTF8.GetString(x))))
                .ToArray();
            return anagrams.ToArray();
        }

        public static IEnumerable<Byte[]> SplitByteArray(IEnumerable<Byte> source, byte marker) {
            if (null == source)
                throw new ArgumentNullException("source");

            List<byte> current = new List<byte>();

            foreach (byte b in source) {
                if (b == marker) {
                    if (current.Count > 0)
                        yield return current.ToArray();

                    current.Clear();
                }

                current.Add(b);
            }

            if (current.Count > 0)
                yield return current.ToArray();
        }
    }

    public class AnagramalistParrallelGrouping_CustomStruct_Bytes_2 : IAnagramalist
    {
        public string[] FindAllAnagrams(byte[] bytes)
        {
            var splitByteArray = AnagramalistParrallelGrouping_CustomStruct_Bytes
                .SplitByteArray(bytes, Encoding.UTF8.GetBytes("\n").Single());

            var anagrams = splitByteArray
                .AsParallel()
                .GroupBy(word => IRepresentOrderdString.FromBytes(word))
                .Where(group => group.Count() > 1)
                .Select(group =>
                {
                    var array = @group.ToArray();
                    var totalLength = array.Sum(word => word.Length) + array.Length;
                    var concatenated = new byte[totalLength];
                    int indexInConcatenated = 0;
                    foreach (var word in array)
                    {
                        word.CopyTo(concatenated, indexInConcatenated);
                        concatenated[word.Length] = (byte) ' ';
                        indexInConcatenated += word.Length + 1;
                    }

                    return Encoding.UTF8.GetString(concatenated);
                })
                .ToArray();
            return anagrams.ToArray();
        }
    }
}