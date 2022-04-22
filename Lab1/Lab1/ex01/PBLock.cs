using System;
using System.Linq;

namespace Lab1.ex01
{
    public sealed class PBlockTesting
    {
        private static byte[] CreateBytesArray(uint size)
        {
            Random rnd = new Random();
            byte[] bytes = new byte[size];
            for (uint i = 0; i < size; ++i)
            {
                bytes[i] = (byte)rnd.Next(0, 2);
            }
            return bytes;
        }

        private static byte[] CreateRule(uint size)
        {
            Random rnd = new Random();
            var numbers = Enumerable.Range(0, (int)size).OrderBy(_ => rnd.Next()).Take((int)size).ToList();
            var rule = numbers.Select(x => (byte)x).ToArray();
            return rule;
        }
        
        internal static void TestPBlock(uint testCount, uint size)
        {
            for (uint i = 0; i < testCount; ++i)
            {
                var bytes = PBlockTesting.CreateBytesArray(size);
                var rule = PBlockTesting.CreateRule(size);
                var newBytes = PBlockClass.PBlock(bytes, rule);

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
                Console.WriteLine("\n---\n");
            }
        }
    }

    public sealed class PBlockClass
    {
        public static byte[] PBlock(in byte[] bytes, in byte[] rule)
        {
            if (bytes.Length == 0)
                throw new Exception("Bytes array cannot be empty");
            if (rule.Length == 0)
                throw new Exception("Rule array cannot be empty");
            if (bytes.Length != rule.Length)
                throw new Exception("Length of bytes array and rule array must be the same");
            
            byte[] newBytes = new byte[bytes.Length];
            for (uint i = 0; i < rule.Length; i++)
            {
                newBytes[i] = bytes[rule[i]];
            }
            return newBytes;
        }
    }
}