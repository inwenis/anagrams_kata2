using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anagramalist.Implementations
{
    public class AnagramalistDictionary : IAnagramalist
    {
        public string[] FindAllAnagrams(byte[] bytes)
        {
            var allText = Encoding.UTF8.GetString(bytes);
            var words = allText.Split('\n');

            var dic = new Dictionary<string, string>();

            foreach (var word in words.Where(x => x.Length > 1))
            {
                string ordered = new string(word.OrderBy(c => c).ToArray());

                if (dic.ContainsKey(ordered))
                {
                    dic[ordered] += " " + word;
                }
                else
                {
                    dic[ordered] = word;
                }
            }

            return dic.Values.Where(x => x.Contains(" ")).ToArray();
        }
    }
}