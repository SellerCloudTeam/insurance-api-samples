using Newtonsoft.Json;
using System;

namespace Sample.DotNetCore
{
    static class Log
    {
        public static void Line(string text)
        {
            Console.WriteLine(text);
            Console.WriteLine();
        }

        public static void Dump(string title, string text)
        {
            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length));
            Console.WriteLine();
            Console.WriteLine(text);
            Console.WriteLine();
        }

        public static void Dump<T>(string title, T value)
        {
            string json = JsonConvert.SerializeObject(value, Formatting.Indented);

            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length));
            Console.WriteLine();
            Console.WriteLine(json);
            Console.WriteLine();
        }
    }
}
