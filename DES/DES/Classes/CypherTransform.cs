using System;
using System.Reflection.Metadata;
using System.Xml.XPath;
using DES.Interfaces;
using DES.Utils;

namespace DES.Classes
{
    internal class CypherTransform : ICypherTransform
    {
        public byte[] Transform(byte[] block, byte[] roundKey)
        {
            var permutedBlock = BlockUtils.Permute32(block, Constants.ExpandingPermutation);
            var expandedBlock = BitConverter.ToUInt64(permutedBlock) ^ BitConverter.ToUInt64(roundKey);
            
            ulong transformedBlock = 0;
            for (int i = 0; i < 8; ++i)
            {
                var sBlock = expandedBlock >> (i * 6) & (1 << 6) - 1;
                var edgeBits = ((sBlock >> 5) << 1) | (sBlock & 1);
                var middleBits = (sBlock >> 1) & ((1 << 4) - 1);
                
                sBlock = Constants.STables[i, edgeBits, middleBits];
                transformedBlock |= sBlock << i * 4;
            }
            return BitConverter.GetBytes(transformedBlock);
        }
    }
}