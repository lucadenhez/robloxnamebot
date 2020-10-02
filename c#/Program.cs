using System;
using System.Collections.Generic;
using System.Threading;

namespace robloxnamebot
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            while (count != 10001) {
                count = count + 1;
                var list = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
                var digit_1 = new Random();
                var digit_2 = new Random();
                var digit_3 = new Random();
                var digit_4 = new Random();
                var digit_5 = new Random();
                int index_1 = digit_1.Next(list.Count);
                int index_2 = digit_2.Next(list.Count);
                int index_3 = digit_3.Next(list.Count);
                int index_4 = digit_4.Next(list.Count);
                int index_5 = digit_5.Next(list.Count);
                Console.WriteLine(list[index_1] + list[index_2] + list[index_3] + list[index_4] + list[index_5]);
            }
        }
    }
}
