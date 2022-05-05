using System;
using System.Collections.Generic;
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

        public CypherContext(byte[] key, EncryptionMode mode, byte[] initializationVector = null, params object[] args)
        {
            _key = key;
            _mode = mode;
            _initializationVector = initializationVector;
        }

        public byte[] Encrypt(byte[] block)
        {
            var resultBlock = PaddingPkcs7(block);
            var blocksList = new List<byte[]>();
            switch (_mode)
            {
                case EncryptionMode.ECB:
                {
                    var currBlock = new byte[Constants.BlockSize];
                    for (var i = 0; i < resultBlock.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(resultBlock, i * Constants.BlockSize, currBlock,
                            0, Constants.BlockSize);
                        blocksList.Add(Encrypter.Encrypt(currBlock));
                    }
                    break;
                }
                
                case EncryptionMode.CBC:
                {
                    var prevBlock = new byte[Constants.BlockSize];
                    var nextBlock = new byte[Constants.BlockSize];
                    Array.Copy(_initializationVector, prevBlock, prevBlock.Length);
                    for (var i = 0; i < resultBlock.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(resultBlock, i * Constants.BlockSize, nextBlock,
                            0, Constants.BlockSize);
                        var xorResult = BitConverter.ToUInt64(nextBlock) ^ BitConverter.ToUInt64(prevBlock);
                        blocksList.Add(Encrypter.Encrypt(BitConverter.GetBytes(xorResult)));
                        Array.Copy(blocksList[i], prevBlock, Constants.BlockSize);
                    }
                    break;
                }
                
                case EncryptionMode.CFB:
                {
                    var prevBlock = new byte[Constants.BlockSize];
                    var nextBlock = new byte[Constants.BlockSize];
                    Array.Copy(_initializationVector, prevBlock, prevBlock.Length);
                    for (var i = 0; i < resultBlock.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(resultBlock, i * Constants.BlockSize, nextBlock,
                            0, Constants.BlockSize);
                        var xorResult = BitConverter.ToUInt64(Encrypter.Encrypt(prevBlock))
                                        ^ BitConverter.ToUInt64(nextBlock);
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                        Array.Copy(blocksList[i], prevBlock, Constants.BlockSize);
                    }
                    break;
                }

                case EncryptionMode.OFB:
                {
                    var prevBlock = new byte[Constants.BlockSize];
                    var nextBlock = new byte[Constants.BlockSize];
                    Array.Copy(_initializationVector, prevBlock, prevBlock.Length);
                    for (var i = 0; i < resultBlock.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(resultBlock, i * Constants.BlockSize, nextBlock,
                            0, Constants.BlockSize);
                        var encryptedBlock = Encrypter.Encrypt(prevBlock);
                        var xorResult = BitConverter.ToUInt64(encryptedBlock) ^ BitConverter.ToUInt64(nextBlock);
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                        Array.Copy(encryptedBlock, prevBlock, Constants.BlockSize);
                    }
                    break;
                }
                
                case EncryptionMode.CTR:
                {
                    var IV = new byte[Constants.BlockSize];
                    _initializationVector.CopyTo(IV, 0);
                    var counter = BitConverter.ToUInt64(IV);
                    var currBlock = new byte[Constants.BlockSize];
                    for (int i = 0; i < resultBlock.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(resultBlock, i * Constants.BlockSize, currBlock,
                            0, Constants.BlockSize);
                        var xorResult = BitConverter.ToUInt64(Encrypter.Encrypt(IV)) ^
                                        BitConverter.ToUInt64(currBlock);
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                        IV = BitConverter.GetBytes(++counter);
                    }
                    break;
                }
                
                case EncryptionMode.RD:
                    break;
                
                case EncryptionMode.RDH:
                    break;
            }
            for (int i = 0; i < blocksList.Count; ++i)
            {
                Array.Copy(blocksList[i], 0, resultBlock,
                    i * Constants.BlockSize, Constants.BlockSize);
            }
            return resultBlock;
        }

        public byte[] Decrypt(byte[] block)
        {
            var blocksList = new List<byte[]>();
            switch (_mode)
            {
                case EncryptionMode.ECB:
                {
                    var currBlock = new byte[Constants.BlockSize];
                    for (var i = 0; i < block.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(block, i * Constants.BlockSize, currBlock,
                            0, Constants.BlockSize);
                        blocksList.Add(Encrypter.Decrypt(currBlock));
                    }

                    break;
                }
                
                case EncryptionMode.CBC:
                {
                    var prevBlock = new byte[Constants.BlockSize];
                    var curBlock = new byte[Constants.BlockSize];
                    Array.Copy(_initializationVector, prevBlock, prevBlock.Length);
                    for (var i = 0; i < block.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(block, i * Constants.BlockSize, curBlock,
                            0, Constants.BlockSize);
                        var xorResult = BitConverter.ToUInt64(prevBlock) ^
                                        BitConverter.ToUInt64(Encrypter.Decrypt(curBlock));
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                        Array.Copy(curBlock, prevBlock, Constants.BlockSize);
                    }
                    break;
                }

                case EncryptionMode.CFB:
                {
                    var prevBlock = new byte[Constants.BlockSize];
                    var nextBlock = new byte[Constants.BlockSize];
                    Array.Copy(_initializationVector, prevBlock, prevBlock.Length);
                    for (var i = 0; i < block.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(block, i * Constants.BlockSize, nextBlock,
                            0, Constants.BlockSize);
                        var xorResult = BitConverter.ToUInt64(Encrypter.Encrypt(prevBlock)) ^
                                        BitConverter.ToUInt64(nextBlock);
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                        Array.Copy(nextBlock, prevBlock, Constants.BlockSize);
                    }
                    break;
                }
                
                case EncryptionMode.OFB:
                {
                    var prevBlock = new byte[Constants.BlockSize];
                    var curBlock = new byte[Constants.BlockSize];
                    Array.Copy(_initializationVector, prevBlock, prevBlock.Length);
                    for (var i = 0; i < block.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(block, i * Constants.BlockSize, curBlock,
                            0, Constants.BlockSize);
                        var encryptBlock = Encrypter.Encrypt(prevBlock);
                        var xorResult = BitConverter.ToUInt64(encryptBlock) ^ BitConverter.ToUInt64(curBlock);
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                        Array.Copy(encryptBlock, prevBlock, Constants.BlockSize);
                    }
                    break;
                }
                
                case EncryptionMode.CTR:
                {
                    var IV = new byte[Constants.BlockSize];
                    _initializationVector.CopyTo(IV, 0);
                    var counter = BitConverter.ToUInt64(IV);
                    var currBlock = new byte[Constants.BlockSize];
                    for (var i = 0; i < block.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(block, i * Constants.BlockSize, currBlock,
                            0, Constants.BlockSize);
                        var xorResult = BitConverter.ToUInt64(Encrypter.Encrypt(IV)) ^
                                        BitConverter.ToUInt64(currBlock);
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                        IV = BitConverter.GetBytes(++counter);
                    }
                    break;
                }
            }
            
            var connectedBlock = new byte[Constants.BlockSize * blocksList.Count];
            for (var i = 0; i < blocksList.Count; ++i)
            {
                Array.Copy(blocksList[i], 0, connectedBlock,
                        i * Constants.BlockSize, Constants.BlockSize);
            }
            var decryptedBlock = new byte[connectedBlock.Length - connectedBlock[connectedBlock.Length - 1]];
            Array.Copy(connectedBlock, decryptedBlock, decryptedBlock.Length);
            return decryptedBlock;
        }

        private byte[] PaddingPkcs7(byte[] block)
        {
            byte mod = (byte)(Constants.BlockSize - block.Length % Constants.BlockSize);
            mod = (byte)(mod == 0 ? Constants.BlockSize : mod);
            
            var paddedBlock = new byte[block.Length + mod];
            Array.Copy(block, paddedBlock, block.Length);
            Array.Fill(paddedBlock, mod, block.Length, mod); 
            return paddedBlock;
        }
    }
}