using System.Linq;
using System.Text;
using test_string_vs_struct;

namespace Anagramalist.Implementations
{
    public class AnagramalistParrallelGrouping_CustomStruct_Bytes : IAnagramalistFromBytes
    {
        public string[] FindAllAnagrams(byte[][] bytes)
        {
            var anagrams = bytes
                .AsParallel()
                .GroupBy(word => IRepresentOrderdString.FromBytes(word))
                .Where(group => group.Count() > 1)
                .Select(group => string.Join(" ", group.Select(x => Encoding.UTF8.GetString(x))))
                .ToArray();
            return anagrams.ToArray();
        }
    }

    public class AnagramalistParrallelGrouping_CustomStruct_Bytes_2 : IAnagramalistFromBytes
    {
        public string[] FindAllAnagrams(byte[][] bytes)
        {
            var anagrams = bytes
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