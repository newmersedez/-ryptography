using System;
using System.Collections;
using System.Collections.Generic;

namespace DES.Utils
{
    public static class BlockUtils
    {
        public static byte[] PermuteBlock(byte[] block, byte[] rule)
        {
            var number = BitConverter.ToInt32(block);
            if (Math.ILogB(number) + 1 != rule.Length)
                throw new ArgumentException("Block and rule arrays have different sizes");
            var permutatedBlock = 0;
            for (int i = 0; i < rule.Length; ++i)
            {
                permutatedBlock |= ((number >> (rule[i] - 1)) & 1) << i;
            }
            return BitConverter.GetBytes(permutatedBlock);
        }

        public static byte[] SubstituteBlock(byte[] block, Dictionary<byte, byte> rule, int count)
        {
            var number = BitConverter.ToInt32(block);
            if ((Math.ILogB(number) + 1) % rule.Count != 0)
                throw new ArgumentException("Incorrect substitute rule");
            var substitutedBlock = 0;
            for (int i = 0; i < rule.Count; ++i)
            {
                var keyBlock = (byte)((number >> i * count) & ((1 << count) - 1));
                var sBlock = rule[keyBlock];
                substitutedBlock = substitutedBlock | (sBlock << i * count);
            }
            return BitConverter.GetBytes(substitutedBlock);
        }
    }
}