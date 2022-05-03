using System;
using System.Collections.Generic;

namespace DES.Utils
{
    public static class BlockUtils
    {
        public static byte[] Permute64(byte[] block, byte[] rule)
        {
            var number = BitConverter.ToUInt64(block, 0);
            ulong permutedBlock = 0;
            for (int i = 0; i < rule.Length; ++i)
            {
                permutedBlock |= ((number >> (rule[i] - 1)) & 1) << i;
            }
            return BitConverter.GetBytes(permutedBlock);
        }
        
        public static byte[] Permute32(byte[] block, byte[] rule)
        {
            var number = BitConverter.ToUInt32(block, 0);
            ulong permutedBlock = 0;
            for (int i = 0; i < rule.Length; ++i)
            {
                permutedBlock |= ((number >> (rule[i] - 1)) & 1) << i;
            }
            return BitConverter.GetBytes(permutedBlock);
        }
        
        public static byte[] Substitute64(byte[] block, Dictionary<byte, byte> rule, int count)
        {
            var number = BitConverter.ToUInt64(block, 0);
            ulong substitutedBlock = 0;
            for (int i = 0; i < rule.Count; ++i)
            {
                var keyBlock = (byte)((number >> i * count) & (ulong)((1 << count) - 1));
                var sBlock = rule[keyBlock];
                substitutedBlock |= (ulong)sBlock << (i * count);
            }
            return BitConverter.GetBytes(substitutedBlock);
        }
        
        public static byte[] Substitute32(byte[] block, Dictionary<byte, byte> rule, int count)
        {
            var number = BitConverter.ToUInt32(block, 0);
            ulong substitutedBlock = 0;
            for (int i = 0; i < rule.Count; ++i)
            {
                var keyBlock = (byte)((number >> i * count) & (ulong)((1 << count) - 1));
                var sBlock = rule[keyBlock];
                substitutedBlock |= (ulong)sBlock << (i * count);
            }
            return BitConverter.GetBytes(substitutedBlock);
        }
    }
}