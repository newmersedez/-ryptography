using System;
using DES.Interfaces;
using DES.Utils;

namespace DES.Classes
{
    public sealed class DES : FeistelNetwork
    {
        public DES(IExpandKey keyGen, ICypherTransform cypherEncrypter) : base(keyGen, cypherEncrypter)
        { }

        public byte[] EncryptMessage(byte[] message)
        {
            var resultBlock = BlockUtils.Permute64(message, Constants.DesStartPermutation);
            resultBlock = Encrypt(resultBlock);
            resultBlock = BlockUtils.Permute64(resultBlock, Constants.DesEndPermutation);
            return resultBlock;
        }

        public byte[] DecryptMessage(byte[] message)
        {
            var resultBlock = BlockUtils.Permute64(message, Constants.DesStartPermutation);
            resultBlock = Decrypt(resultBlock);
            resultBlock = BlockUtils.Permute64(resultBlock, Constants.DesEndPermutation);
            return resultBlock;
        }
    }
}