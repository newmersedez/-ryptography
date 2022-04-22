using System;

namespace Lab1
{
    public static class Program2
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Testing random Pblocks:");
            ex01.PBlockTesting.TestPBlock(3, 5);
            Console.WriteLine("Testing random Sblocks:");
            ex02.SBlockTesting.TestSBlock(3, 8, 2);
        }
    }
}