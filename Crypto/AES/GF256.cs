using System;
using System.Numerics;

namespace AES
{
    public static class GF256
    {
        #region PublicMethods

        public static byte Add(byte first, byte second)
        {
            return (byte)(first ^ second);
        }
        
        public static byte Multipy(byte first, byte second, UInt16 modulo)
        {
            byte result = 0;
            while (first != 0 && second != 0)
            {
                if ((second & 1) != 0)
                    result ^= first;
                if ((first & 0x80) != 0)
                    first = (byte)((first << 1) ^ modulo);
                else
                    first <<= 1;
                second >>= 1;
            }
            return result;
        }
        
        public static byte Inverse(byte polynomial, UInt16 modulo)
        {
            UInt16 degree = 254;
            byte result = 1;
            UInt16 bitesForMask = degree;
            while (bitesForMask > 0)
            {
                if ((bitesForMask & 0b01) == 1)
                {
                    result = Modulo(Multipy(result, polynomial, modulo), modulo);
                }
                bitesForMask >>= 1;
                polynomial = Multipy(polynomial, polynomial, modulo);
            }
            return result;
        }

        public static bool CheckIrreducibility(byte polynomial)
        {
            for (uint i = 2; i < polynomial; ++i)
            {
                for (uint j = 2; j < polynomial; ++j)
                {
                    uint l = 0b10000000000, r = 0b10000000000;
                    uint midResult = 0;
                    while (l != 0)
                    {
                        while (r != 0)
                        {
                            midResult ^= PolyMultiply(i & l, j & r);
                            r >>= 1;
                        }
                        l >>= 1;
                        r = 0b10000000;
                    }
                    if (midResult == polynomial)
                            return false;
                }
            }
            
            return true;
        }

        public static byte[] GetIrreduciblePolynomials(byte polynomial)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region PrivateMethods

        private static UInt16 ModPolyMultiply(UInt16 first, UInt16 second, UInt16 modulo)
        {
            UInt16 result = 0;
            while (first != 0)
            {
                if ((first & 1) != 0)
                    result = (UInt16)((result + second) % modulo);
                second = (UInt16)((second << 1) % modulo);
                first >>= 1;
            }
            return (result);
        }
        
        private static uint PolyMultiply(uint first, uint second)
        {
            var result = first;
            if (second == 0)
                return 0;
            while (second != 1)
            {
                result <<= 1;
                second >>= 1;
            }
            return result;
        }
        
        private static UInt16 GetHigherBits(UInt16 polynomial)
        {
            if (polynomial == 0)
                return 0;
            UInt16 result = 1;
            while (polynomial != 1)
            {
                polynomial >>= 1;
                result <<= 1;
            }

            return result;
        }
        
        private static byte Modulo(UInt16 polynomial, UInt16 modulo)
        {
            UInt16 polynomialDegree = GetHigherBits(polynomial);
            UInt16 moduloDegree = GetHigherBits(modulo);

            while (polynomialDegree >= moduloDegree)
            {
                UInt16 tmpLeft = polynomialDegree;
                UInt16 tmpRight = moduloDegree;
                while (tmpRight != 1)
                {
                    tmpLeft >>= 1;
                    tmpRight >>= 1;
                }

                UInt16 tmpMulResult = ModPolyMultiply(modulo, tmpLeft, modulo);
                polynomial ^= tmpMulResult;
                polynomialDegree = GetHigherBits(polynomial);
            }
            return (byte)polynomial;
        }
        
        #endregion
    }
}