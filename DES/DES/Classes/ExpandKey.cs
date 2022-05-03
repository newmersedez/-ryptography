using System;
using DES.Interfaces;
using DES.Utils;

namespace DES.Classes
{
    internal class ExpandKey : IExpandKey
    {
        public byte[][] GenerateRoundKeys(byte[] key)
        {
            byte[][] roundKeys = new byte[16][];
            byte[] permutedKey = BlockUtils.Permute64(Constants.KeyStartPermutation, key);
            var number = BitConverter.ToUInt64(permutedKey, 0);
            var c = number >> 28;
            var d = number & ((1 << 28) - 1);
            
            for (int round = 0; round < 16; ++round)
            {
                var shift = Constants.KeyLeftCircularShift[round];
                c = ((c << shift) | (c >> (28 - shift))) & ((1 << 28) - 1);
                d = ((d << shift) | (d >> (28 - shift))) & ((1 << 28) - 1);
                roundKeys[round] = BitConverter.GetBytes((c << 28) | d);
            }
            return roundKeys;
        }
    }
}