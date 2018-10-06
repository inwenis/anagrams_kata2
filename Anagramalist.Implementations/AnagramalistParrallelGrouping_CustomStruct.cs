using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_string_vs_struct;

namespace Anagramalist.Implementations
{
    public class AnagramalistParrallelGrouping_CustomStruct_Bytes
    {
        public byte[][][] FindAllAnagrams(byte[][] bytes)
        {
            var anagrams = bytes
                .AsParallel()
                .GroupBy(w => IRepresentOrderdString.FromBytes(w))
                .Where(g => g.Count() > 1)
                .Select(group =>
                {
                    var array = group.ToArray();
//                    var sum = array.Sum(word => word.Length);
//                    sum += array.Length;
//                    var total = new byte[sum];
//
//                    int indexInTotal = 0;
//                    foreach (var word in array)
//                    {
//                        word.CopyTo(total, indexInTotal);
//                        total[word.Length] = (byte) ' ';
//                        indexInTotal += word.Length + 1;
//                    }

                    return array;
                })
                .ToArray();
            return anagrams.ToArray();
        }
    }


    public class AnagramalistParrallelGrouping_CustomStruct : IAnagramalist
    {
        public string[] FindAllAnagrams(string[] words)
        {
            var anagrams = words
                .AsParallel()
                .GroupBy(w => IRepresentOrderdString.FromString(w))
                .Where(g => g.Count() > 1)
                .Select(x => string.Join(" ", x))
                .ToArray();
            return anagrams.ToArray();

        }
    }
}