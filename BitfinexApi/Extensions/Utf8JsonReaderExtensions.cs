using System.Text.Json;

namespace BitfinexApi.Extensions
{
    static class Utf8JsonReaderExtensions
    {
        public static void Read(this ref Utf8JsonReader utf8JsonReader, int number)
        {
            for (int i = 0; i < number; i++)
            {
                utf8JsonReader.Read();
            }
        }

        public static bool ReadToEnd(this ref Utf8JsonReader utf8JsonReader, int startDepth)
        {
            while (utf8JsonReader.Read())
            {
                if (utf8JsonReader.CurrentDepth == startDepth) return true;
            }

            return true;
        }
    }
}
