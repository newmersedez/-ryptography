using System;
using DES.Interfaces;
using DES.Utils;

namespace DES.Classes
{
    public sealed class DES : FeistelNetwork
    {
        public DES() : base(new ExpandKey(), new CypherTransform())
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
            var resultBlock = BlockUtils.Permute64(message, Constants.DesEndPermutation);
            resultBlock = Decrypt(resultBlock);
            resultBlock = BlockUtils.Permute64(resultBlock, Constants.DesStartPermutation);
            return resultBlock;
        }
    }
}