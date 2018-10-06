using System.Linq;
using System.Text;

namespace Anagramalist.Implementations
{
    public class AnagramalistLinq : IAnagramalist
    {
        public string[] FindAllAnagrams(byte[] bytes)
        {
            var allText = Encoding.UTF8.GetString(bytes);
            var words = allText.Split('\n');

            var anagrams = words
                .GroupBy(w => new string(w.OrderBy(c => c).ToArray()))
                .Where(g => g.Count() > 1)
                .Select(x => string.Join(" ", x))
                .ToArray();
            return anagrams.ToArray();
        }
    }
}