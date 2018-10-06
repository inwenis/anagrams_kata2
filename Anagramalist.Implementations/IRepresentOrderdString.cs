namespace test_string_vs_struct
{
    /// <summary>
    /// To understand what this struct does checkout the tests.
    /// But basically it's used to test if two words are anagrams.
    /// If IRepresentOrderdString.FromString(wordA) == IRepresentOrderdString.FromString(wordB) is ture then wordA and wordB are anagrams
    /// There is a limitaion - each character can appear at most 10 times in the word, with the exception of character 'z'.
    /// Character 'z' can appear only once.
    /// If characters appear more than 10 times or the 'z' character appers more than once the comparison might not work correctly.
    /// However the 'z' problem can be solved by:
    /// a) adding a fourth ulong field
    /// b) using decimal places that are not used in the ulong `two`
    /// </summary>
    public struct IRepresentOrderdString
    {
        public ulong one;
        public ulong two;
        public ulong three;

        public override string ToString()
        {
            return $"{three:D20}{two:D20}{one:D20}";
        }

        public static bool operator ==(IRepresentOrderdString c1, IRepresentOrderdString c2)
        {
            return c1.one == c2.one && c1.two == c2.two && c1.three == c2.three;
        }

        public static bool operator !=(IRepresentOrderdString c1, IRepresentOrderdString c2)
        {
            return !(c1 == c2);
        }

        public static IRepresentOrderdString FromString(string word)
        {
            IRepresentOrderdString x = new IRepresentOrderdString();
            for (int i = 0; i < word.Length; i++)
            {
                var index = word[i] - 'A';
                if (index < 0) //store all characters that come beofe 'A' in the 7th decimal place in the socond ulong character
                {
                    x.two += Math.Pow(10, 7);
                }
                else if (index <= 18) //store the count of characters A-S in the fist ulong
                {
                    x.one += Math.Pow(10, index);
                }
                else if(index <= 37) //store the count of characters T-Z a-f in the second ulong
                {
                    index -= 19;
                    x.two += Math.Pow(10, index);
                }
                else if(index <= 57) //store the count of characters g-z in the third ulong
                {
                    index -= 38;
                    x.three += Math.Pow(10, index);
                }
                else //store all other/special characters count in the 9th decimal place in the second ulong
                {
                    x.two += Math.Pow(10, 8);
                }
            }

            return x;
        }

        public static IRepresentOrderdString FromBytes(byte[] bytes)
        {
            IRepresentOrderdString x = new IRepresentOrderdString();
            for (int i = 0; i < bytes.Length; i++)
            {
                var index = bytes[i] - 'A';
                if (index < 0) //store all characters that come beofe 'A' in the 7th decimal place in the socond ulong character
                {
                    x.two += Math.Pow(10, 7);
                }
                else if (index <= 18) //store the count of characters A-S in the fist ulong
                {
                    x.one += Math.Pow(10, index);
                }
                else if(index <= 37) //store the count of characters T-Z a-f in the second ulong
                {
                    index -= 19;
                    x.two += Math.Pow(10, index);
                }
                else if(index <= 57) //store the count of characters g-z in the third ulong
                {
                    index -= 38;
                    x.three += Math.Pow(10, index);
                }
                else //store all other/special characters count in the 9th decimal place in the second ulong
                {
                    x.two += Math.Pow(10, 8);
                }
            }

            return x;
        }

    }
}