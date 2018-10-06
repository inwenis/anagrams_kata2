using System.Linq;
using System.Text;
using Anagramalist.Implementations;
using NUnit.Framework;

[TestFixture]
public class AnagramalistTests
{
    static object[] SystemsToTest = new IAnagramalist[] 
    {
        new AnagramalistLinq(), 
        new AnagramalistParallelLinq(),
        new AnagramalistDictionary(),
        new AnagramalistParrallelGrouping_CustomStruct(), 
    };

    [Test, TestCaseSource("SystemsToTest")]
    public void FindAllAnagrams_NoWords_ReturnsEmptyResult(IAnagramalist sut)
    {
        var result = sut.FindAllAnagrams(Encoding.UTF8.GetBytes(string.Join("\n", new string[0])));

        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    [Test, TestCaseSource("SystemsToTest")]
    public void FindAllAnagrams_OnePossibleAnagramInInput_ReturnsAnagram(IAnagramalist sut)
    {
        string[] input = new []{"word", "drow"};

        var result = sut.FindAllAnagrams(Encoding.UTF8.GetBytes(string.Join("\n", input)));

        Assert.IsTrue(result.Any(x => x.Contains("word") && x.Contains("drow")));
    }

    [Test, TestCaseSource("SystemsToTest")]
    public void FindAllAnagrams_3WordsInInputWithSameLetters_Returns3WordsAnagram(IAnagramalist sut)
    {
        string[] input = new []{"word", "drow", "drwo"};

        var result = sut.FindAllAnagrams(Encoding.UTF8.GetBytes(string.Join("\n", input)));

        Assert.IsTrue(result.Any(x => x.Contains("word") && x.Contains("drwo") && x.Contains("drow")));
    }

    [Test, TestCaseSource("SystemsToTest")]
    public void FindAllAnagrams_3WordsInInputButOnlyTwoHaveSameLetters_Returns2WordsAnagram(IAnagramalist sut)
    {
        string[] input = new []{"word", "drow", "x"};

        var result = sut.FindAllAnagrams(Encoding.UTF8.GetBytes(string.Join("\n", input)));

        Assert.IsTrue(result.Any(x => x.Contains("word") && x.Contains("drow") ));
    }

    [Test, TestCaseSource("SystemsToTest")]
    public void FindAllAnagrams_NonMachingWordInInputWithSameLength_Returns2WordsAnagram(IAnagramalist sut)
    {
        string[] input = new []{"word", "drow", "xxxx"};

        var result = sut.FindAllAnagrams(Encoding.UTF8.GetBytes(string.Join("\n", input)));

        Assert.IsTrue(result.Any(x => x.Contains("word") && x.Contains("drow") ));
    }

    [Test, TestCaseSource("SystemsToTest")]
    public void FindAllAnagrams_2PossibleAnagramsInInput_Returns2Anagrams(IAnagramalist sut)
    {
        string[] input = new []{"word", "drow", "dog", "god"};

        var result = sut.FindAllAnagrams(Encoding.UTF8.GetBytes(string.Join("\n", input)));

        Assert.IsTrue(result.Any(x => x.Contains("dog") && x.Contains("god")));
        Assert.IsTrue(result.Any(x => x.Contains("word") && x.Contains("drow")));
    }

    [Test, TestCaseSource("SystemsToTest")]
    public void FindAllAnagrams_WordsHasSameLengthButThereIsNoAnagrams_ReturnsEmptyArray(IAnagramalist sut)
    {
        string[] input = new []{"word", "yyyy", "xxxx"};

        var result = sut.FindAllAnagrams(Encoding.UTF8.GetBytes(string.Join("\n", input)));

        Assert.IsEmpty(result);
    }

    [Test, TestCaseSource("SystemsToTest")]
    public void FindAllAnagrams_FirstWordHasSameLengthButHasDifferentLetters_AnagramIsReturnedAnyway(IAnagramalist sut)
    {
        string[] input = new []{"xxxx", "word", "drow"};

        var result = sut.FindAllAnagrams(Encoding.UTF8.GetBytes(string.Join("\n", input)));

        CollectionAssert.AreEqual(new []{"word drow"}, result);
    }
}
