using System;
using System.Linq;

namespace Lab1
{
    class Program
    {
        
        private static byte[] CreateBytesArray(int size)
        {
            if (size == 0)
                throw new Exception("Size must be greater than 0");
            
            Random rnd = new Random();
            byte[] bytes = new byte[size];
            for (int i = 0; i < size; ++i)
            {
                bytes[i] = (byte)rnd.Next(0, 2);
            }
            return bytes;
        }

        private static int[] CreateRuleArray(int size)
        {
            if (size == 0)
                throw new Exception("Size must be greater than 0");

            Random rnd = new Random();
            var numbers = Enumerable.Range(0, size).OrderBy(x => rnd.Next()).Take(size);
            int[] rule = numbers.ToArray();
            return rule;
        }

        private static void TestPBlock(int testCount, int size)
        {
            for (int i = 0; i < testCount; ++i)
            {
                var bytes = CreateBytesArray(size);
                var rule = CreateRuleArray(size);
                var newBytes = PBlock(bytes, rule);

                Console.Write("Bytes array: ");
                foreach (var elem in bytes)
                {
                    Console.Write(elem);
                }
                Console.WriteLine();
                
                Console.Write("Rule array: ");
                foreach (var elem in rule)
                {
                    Console.Write(elem);
                }
                Console.WriteLine();
                
                Console.Write("New bytes array: ");
                foreach (var elem in newBytes)
                {
                    Console.Write(elem);
                }
                Console.WriteLine("\n");
            }
        }

        public static byte[] PBlock(in byte[] bytes, in int[] rule)
        {
            if (bytes.Length != rule.Length)
                throw new Exception("Length of bytes array and rule array must be the same");
            
            byte[] newBytes = new byte[bytes.Length];
            for (int i = 0; i < rule.Length; ++i)
            {
                newBytes[rule[i]] = bytes[i];
            }
            return newBytes;
        }
        
        public static void Main(string[] args)
        {
            TestPBlock(10, 5);
        }
    }
}