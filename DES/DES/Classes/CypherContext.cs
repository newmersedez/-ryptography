using System;
using System.Numerics;
using DES.Interfaces;

namespace DES.Classes
{
    internal enum EncryptionMode
    {
        Ecb,
        Cbc,
        Cfb,
        Ofb,
        Ctr,
        Rd,
        Rdh
    };

    internal class CypherContext
    {
        private byte[] _key;
        private EncryptionMode _mode;
        private byte[] _initializationVector;
        public IExpandKey KeyGenerator { get; set; }
        public ICypherTransform CypherEncrypter { get; set; }
        
        public CypherContext(byte[] key, EncryptionMode mode, byte[] initializationVector)
        {
            this._key = key;
            this._mode = mode;
            this._initializationVector = initializationVector;
        }

        public byte[] Encrypt(byte[] block)
        {
            throw new NotImplementedException();
        }

        public byte[] Decrypt(byte[] block)
        {
            throw new NotImplementedException();
        }

        private byte[] PaddingPkcs7(byte[] block)
        {
            throw new NotImplementedException();
        }
    }
}