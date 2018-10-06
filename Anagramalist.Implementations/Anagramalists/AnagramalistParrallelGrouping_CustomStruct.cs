using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_string_vs_struct;

namespace Anagramalist.Implementations
{
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