namespace EnigmaUI.Helpers
{
    using System.Collections.Generic;

    public class AlphabetHelper
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
    }
}
