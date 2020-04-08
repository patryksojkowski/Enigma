namespace EnigmaLibrary.Helpers
{
    using System.IO;

    public static class JsonHelper
    {
        public static string GetJsonContent(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
