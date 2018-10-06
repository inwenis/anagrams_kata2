using System.Linq;
using System.Text;
using test_string_vs_struct;

namespace Anagramalist.Implementations
{
    public class AnagramalistParrallelGrouping_CustomStruct_Bytes
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
}