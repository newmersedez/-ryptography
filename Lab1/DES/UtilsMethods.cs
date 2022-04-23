using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DES
{
    public partial class DES
    {
        public static byte[] PermutationBlock(in byte[] bytes, in int[] pBlock)
        {
            return new byte[12];
        }

        public static byte[] SubstitutionBlock(in byte[] bytes, in Dictionary<byte[], byte[]> sBLock)
        {
            return new byte[12];
        }
    }
}