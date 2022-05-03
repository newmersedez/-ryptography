using System;
using System.Numerics;
using DES.Interfaces;
using DES.Utils;

namespace DES.Classes
{
    internal enum EncryptionMode
    {
        ECB,
        CBC,
        CFB,
        OFB,
        CTR,
        RD,
        RDH
    };

    internal class CypherContext
    {
        private byte[] _key;
        private EncryptionMode _mode;
        private byte[] _initializationVector;
        public IExpandKey KeyGenerator { get; set; }
        public ICypherTransform CypherEncrypter { get; set; }
        
        public CypherContext(byte[] key, EncryptionMode mode, byte[] initializationVector = null)
        {
            this._key = key;
            this._mode = mode;
            this._initializationVector = initializationVector;
        }

        public byte[] Encrypt(byte[] block)
        {
            var encryptedBlock = this.PaddingPkcs7(block);
            switch (this._mode)
            {
                case EncryptionMode.ECB:
                    break;
                
                case EncryptionMode.CBC:
                    break;
                
                case EncryptionMode.CFB:
                    break;
                
                case EncryptionMode.OFB:
                    break;
                
                case EncryptionMode.CTR:
                    break;
                
                case EncryptionMode.RD:
                    break;
                
                case EncryptionMode.RDH:
                    break;
            }
            return encryptedBlock;
        }

        public byte[] Decrypt(byte[] block)
        {
            throw new NotImplementedException();
        }

        public byte[] PaddingPkcs7(byte[] block)
        {
            var mod = block.Length % Constants.BlockSize;
            mod = (mod == 0) ? 0 : Constants.BlockSize - mod;
            
            var paddedBlock = new byte[block.Length + mod];
            Array.Copy(block, paddedBlock, block.Length);
            Array.Fill(paddedBlock, (byte)mod, block.Length, mod); 
            return paddedBlock;
        }
    }
}