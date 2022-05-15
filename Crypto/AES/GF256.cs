using System;
using System.Numerics;

namespace AES
{
    public static class GF256
    {
        public static byte Add(byte firstPolynomial, byte secondPolynomial)
        {
            return (byte)(firstPolynomial ^ secondPolynomial);
        }
        
        public static byte Multipy(byte firstPolynomial, byte secondPolynomial, UInt16 modulo)
        {
            byte result = 0;
            while (firstPolynomial != 0 && secondPolynomial != 0)
            {
                if ((secondPolynomial & 1) != 0)
                    result ^= firstPolynomial;
                if ((firstPolynomial & 0x80) != 0)
                    firstPolynomial = (byte)((firstPolynomial << 1) ^ modulo);
                else
                    firstPolynomial <<= 1;
                secondPolynomial >>= 1;
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
            UInt16 num = 0;
            UInt16 one = 1;
            
            for (UInt16 i = 0; i < 14; i++)
            {
                if((one & polynomial) != 0)
                    num = i;
                one <<= 1;
            }

            return num == 0;
        }

        public static byte GetIrreduciblePolynomials(byte Polynomial)
        {
            throw new NotImplementedException();
        }
        
        private static UInt16 HigherBits(UInt16 polynomial)
        {
            if (polynomial == 0)
                return 0;
            UInt16 count = 1;
            while (polynomial != 1)
            {
                polynomial >>= 1;
                count <<= 1;
            }

            return count;
        }

        private static UInt16 MultiplyHelper(UInt16 left, UInt16 right)
        {
            if (right == 0)
                return 0;
            while (right != 1)
            {
                left <<= 1;
                right >>= 1;
            }
            return left;
        }
        
        private static byte Modulo(UInt16 num, UInt16 modulo)
        {
            UInt16 lDeg = HigherBits(num);
            UInt16 baseDeg = HigherBits(modulo);

            while (lDeg >= baseDeg)
            {
                UInt16 tmpLeft = lDeg;
                UInt16 tmpRight = baseDeg;
                while (tmpRight != 1)
                {
                    tmpLeft >>= 1;
                    tmpRight >>= 1;
                }

                UInt16 tmpMulResult = MultiplyHelper(modulo, tmpLeft);
                num ^= tmpMulResult;
                lDeg = HigherBits(num);
            }
            
            return (byte)num;
        }
    }
}