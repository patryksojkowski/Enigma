namespace DragAndDrop
{
    using System.Collections.Generic;

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

        public static List<char> GetShiftedAlphabet(int shift)
        {
            var output = new List<char>();
            for (var i = 'A' + shift; i <= 'Z'; i++)
            {
                output.Add((char)i);
            }
            for (var i = 'A'; i < 'A' + shift; i++)
            {
                output.Add(i);
            }
            return output;
        }

        public static int LetterToNumber(char c)
        {
            return c - 65;
        }

        public static char NumberToLetter(int i)
        {
            return (char)(i + 65);
        }

        public static int To0_25Range(int i)
        {
            return ((i % 26) + 26) % 26;
        }
    }
}