using System;
using DES.Interfaces;

namespace DES.Classes
{
    public class FeistelNetwork : ICrypto
    {
        private readonly IExpandKey _keyGenerator;
        private readonly ICypherTransform _cypherTransformer;
        private byte[][] _roundKeys;

        public FeistelNetwork(IExpandKey keyGen, ICypherTransform cypherTransformer)
        {
            _keyGenerator = keyGen;
            _cypherTransformer = cypherTransformer;
        }

        public byte[] Encrypt(byte[] block)
        {
            ulong number = BitConverter.ToUInt64(block, 0);
            uint oldLeft = (uint)(number >> 32);
            uint oldRight = (uint)(number & ((ulong)1 << 32) - 1);
            uint newLeft = 0;
            uint newRight = 0;
            for (int round = 0; round < 16; round++)
            {
                newLeft = oldRight;
                newRight = oldLeft ^ BitConverter.ToUInt32(_cypherTransformer.Transform(BitConverter.GetBytes(oldRight),
                    _roundKeys[round]));
                oldLeft = newLeft;
                oldRight = newRight;
            }
            number = (ulong)newLeft << 32 | newRight;
            return BitConverter.GetBytes(number);
        }

        public byte[] Decrypt(byte[] block)
        {
            ulong res = BitConverter.ToUInt64(block, 0);
            uint left = (uint)(res >> 32);
            uint right = (uint)(res & (((ulong) 1 << 32) - 1));
            uint newLeft = 0;
            uint newRight = 0;
            for (int round = 15; round >= 0; round--)
            {
                newRight = left;
                newLeft = right ^ BitConverter.ToUInt32(_cypherTransformer.Transform(BitConverter.GetBytes(left),
                    _roundKeys[round]));
                left = newLeft;
                right = newRight;
            }
            res = (ulong)newLeft << 32 | newRight;
            return BitConverter.GetBytes(res);
        }

        public void GenerateRoundKeys(byte[] key)
        {
            _roundKeys = _keyGenerator.GenerateRoundKeys(key);
        }
    }
}