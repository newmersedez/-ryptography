using System;
using System.Collections.Generic;

namespace Lab1.ex02
{
    public sealed class SBlockTesting
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
        
        internal static void TestSBlock(uint testCount, uint size, uint blockSize)
        {
            byte[] bytes = SBlockTesting.CreateBytesArray(size);
            var rule = new Dictionary<byte[], byte[]>
            {
                {new byte[] {0, 0}, new byte[] {1, 1, 1}},
                {new byte[] {0, 1}, new byte[] {1, 0, 1}},
                {new byte[] {1, 0}, new byte[] {1, 0, 1}},
                {new byte[] {1, 1}, new byte[] {1, 1, 1}},
            };
            var newBlock = SBlockClass.SBlock(bytes, rule);
        }
    }

    public sealed class SBlockClass
    {
        public static byte[] SBlock(in byte[] bytes, in Dictionary<byte[], byte[]> rule)
        {
            if (bytes.Length == 0)
                throw new Exception("Bytes array cannot be empty");
            if (rule.Count == 0)
                throw new Exception("Rule array cannot be empty");
            
            byte[] newBytes = new byte[bytes.Length];
            uint size = rule.
            return newBytes;
        }
    }
}