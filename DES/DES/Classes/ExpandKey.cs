using System;
using DES.Interfaces;
using DES.Utils;

namespace DES.Classes
{
    internal class ExpandKey : IExpandKey
    {
        public byte[][] GenerateRoundKeys(byte[] key)
        {
            var permutedKey = BlockUtils.Permute64(key, Constants.KeyStartPermutation);
            var number = BitConverter.ToUInt64(permutedKey, 0);

            var c = number >> 28;
            var d = number & ((1 << 28) - 1);

            var roundKeys = new byte[16][];
            for (int round = 0; round < 16; ++round)
            {
                var shift = Constants.KeyLeftCircularShift[round];
                c = ((c << shift) | (c >> (28 - shift))) & ((1 << 28) - 1);
                d = ((d << shift) | (d >> (28 - shift))) & ((1 << 28) - 1);
                
                var k = BitConverter.GetBytes((c << 28) | d);
                roundKeys[round] = BlockUtils.Permute64(k, Constants.KeyEndPermutation);
            }
            return roundKeys;
        }
    }
}