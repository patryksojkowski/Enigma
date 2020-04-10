using System.Collections.Generic;

namespace EnigmaLibrary.Helpers
{
    public class CommonHelper
    {
        public static List<char> GetAlphabet()
        {
            var output = new List<char>();
            for (var i = 'A'; i <= 'Z'; i++)
            {
                output.Add(i);
            }
            return output;
        }

        public static int To0_25Range(int i)
        {
            return ((i % 26) + 26) % 26;
        }

        public static char NumberToLetter(int i)
        {
            return (char)(i + 65);
        }

        public static int LetterToNumber(char c)
        {
            return c - 65;
        }
    }
}
