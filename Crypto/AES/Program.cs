using System;

namespace AES
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                byte a = 3;
                byte b = 5;
                Console.WriteLine(GF256.Add(a, b));
            }

            {
                const UInt16 modulo = 0b100011011;
                byte a = 15;
                byte b = 7;
                Console.WriteLine(GF256.Multipy(a, b, modulo));
            }

            {
                const UInt16 modulo = 0b100011011;
                byte a = 123;
                Console.WriteLine(GF256.Inverse(a, modulo));
            }

            {
                byte a = 11;
                Console.WriteLine(GF256.CheckIrreducibility(a));
            }
        }
    }
}