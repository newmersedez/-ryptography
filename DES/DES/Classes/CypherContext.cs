using System;
using System.Numerics;
using DES.Interfaces;

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
        private IExpandKey _keyExpand;
        private ICypherTransform _cypherTransform;
        
        public CypherContext(byte[] key, EncryptionMode mode)
        {
            this._key = key;
            this._mode = mode;
        }

        public byte[] Encrypt(byte[] block)
        {
            throw new NotImplementedException();
        }

        public byte[] Decrypt(byte[] block)
        {
            throw new NotImplementedException();
        }

    }
}