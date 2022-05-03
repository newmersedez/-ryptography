using System;
using System.Collections;
using System.Security.Principal;
using DES.Interfaces;
using DES.Utils;

namespace DES.Classes
{
    public class SimpleCryptoClass : ICrypto
    {
        public byte[] Encrypt(byte[] block)
        {
            var number = BitConverter.ToUInt64(block);
            if (number % 2 == 0)
                return BitConverter.GetBytes(number / 2);
            return BitConverter.GetBytes((number + 1) / 2);
        }

        public byte[] Decrypt(byte[] block)
        {
            var number = BitConverter.ToUInt64(block);
            if (number % 2 == 0)
                return BitConverter.GetBytes(number * 2);
            return BitConverter.GetBytes((number - 1) * 2);
        }

        public void GetRoundKeys(byte[] key)
        {
            throw new System.NotImplementedException();
        }
    }
}