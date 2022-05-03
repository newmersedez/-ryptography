using System;
using System.Collections.Generic;
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
        private readonly byte[] _key;    
        private readonly EncryptionMode _mode;
        private readonly byte[] _initializationVector;
        public ICrypto Encrypter { get; set; }

        public CypherContext(byte[] key, EncryptionMode mode, byte[] initializationVector = null)
        {
            _key = key;
            _mode = mode;
            _initializationVector = initializationVector;
        }

        public byte[] Encrypt(byte[] block)
        {
            byte[] encryptedBlock = PaddingPkcs7(block);
            List<byte[]> currBlocksList = new List<byte[]>();
            switch (_mode)
            {
                case EncryptionMode.ECB:
                    byte[] currBlock = new byte[Constants.BlockSize];
                    for(int i = 0; i < encryptedBlock.Length / Constants.BlockSize; i++)
                    {
                        Array.Copy(encryptedBlock, i * Constants.BlockSize, currBlock,
                            0, Constants.BlockSize);
                        currBlocksList.Add(Encrypter.Encrypt(currBlock));
                    }
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
            for (int i = 0; i < currBlocksList.Count; i++)
            {
                Array.Copy(currBlocksList[i], 0, encryptedBlock,
                    i * Constants.BlockSize, Constants.BlockSize);
            }
            return encryptedBlock;
        }

        public byte[] Decrypt(byte[] block)
        {
            List<byte[]> currBlocksList = new List<byte[]>();
            switch (_mode)
            {
                case EncryptionMode.ECB:
                {
                    byte[] currBlock = new byte[Constants.BlockSize];
                    for (int i = 0; i < block.Length / Constants.BlockSize; i++)
                    {
                        Array.Copy(block, i * Constants.BlockSize, currBlock,
                            0, Constants.BlockSize);
                        currBlocksList.Add(Encrypter.Decrypt(currBlock));
                    }
                    break;
                }
            }
            byte[] connectedBlock = new byte[Constants.BlockSize * currBlocksList.Count];
            for (int i = 0; i < currBlocksList.Count; i++)
            {
                Array.Copy(currBlocksList[i], 0, connectedBlock,
                        i * Constants.BlockSize, Constants.BlockSize);
            }
            var decryptedBlock = new byte[connectedBlock.Length - 1];
            Array.Copy(connectedBlock, decryptedBlock, decryptedBlock.Length);
            return decryptedBlock;
        }

        private byte[] PaddingPkcs7(byte[] block)
        {
            byte mod = (byte)(Constants.BlockSize - block.Length % Constants.BlockSize);
            mod = (byte)(mod == 0 ? Constants.BlockSize : mod);
            
            var paddedBlock = new byte[block.Length + mod];
            Array.Copy(block, paddedBlock, block.Length);
            Array.Fill(paddedBlock, (byte)mod, block.Length, mod); 
            return paddedBlock;
        }
    }
}