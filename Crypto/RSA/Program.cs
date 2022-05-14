using System;
using System.Numerics;
using RSA.Classes;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new BigInteger(11);
            var test = new FermaPrimalityTest();
            var res = test.SimplicityTest(a, 0.5);
            Console.WriteLine(res);
        }
    }
}