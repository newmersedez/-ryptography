using System;

namespace DES
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 512;
            var bytes = BitConverter.GetBytes(a);
            foreach (var elem in bytes)
            {
                Console.WriteLine(elem);
            }
        }
    }
}