using System;
using DES.Interfaces;

namespace DES.Classes
{
    public class FeistelNetwork : ICrypto
    {
        private readonly IExpandKey _keyGenerator;
        private readonly ICypherTransform _cypherTransformer;
        private byte[][] _roundKeys;

        public FeistelNetwork(IExpandKey keyGen, ICypherTransform cypherEncrypter)
        {
            _keyGenerator = keyGen;
            _cypherTransformer = cypherEncrypter;
        }

        public byte[] Encrypt(byte[] block)
        {
            var number = BitConverter.ToUInt64(block);
            var oldLeft = (uint)(number >> 32);
            var oldRight = (uint)((number >> 32) & (((ulong)1 << 32) - 1));
            uint newLeft = 0, newRight = 0;
            for (var round = 0; round < 16; ++round)
            {
                newLeft = oldRight;
                newRight = oldLeft ^ BitConverter.ToUInt32(_cypherTransformer.Transform(BitConverter.GetBytes(oldRight),
                    _roundKeys[round]));
                oldLeft = newLeft;
                oldRight = newRight;
            }
            number = (number << 32) | newRight;
            return BitConverter.GetBytes(number);
        }

        public byte[] Decrypt(byte[] block)
        {
            var number = BitConverter.ToUInt64(block);
            var oldLeft = (uint)(number >> 32);
            var oldRight = (uint)((number >> 32) & (((ulong)1 << 32) - 1));
            uint newLeft = 0, newRight = 0;
            for (var round = 0; round < 16; ++round)
            {
                newRight = oldLeft;
                newLeft = oldRight ^ BitConverter.ToUInt32(_cypherTransformer.Transform(BitConverter.GetBytes(oldLeft),
                    _roundKeys[round]));
                oldLeft = newLeft;
                oldRight = newRight;
            }
            number = (number << 32) | newRight;
            return BitConverter.GetBytes(number);
        }

        public void GenerateRoundKeys(byte[] key)
        {
            _roundKeys = _keyGenerator.GenerateRoundKeys(key);
        }
    }
}