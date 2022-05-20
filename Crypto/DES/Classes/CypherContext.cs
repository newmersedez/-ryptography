using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DES.Interfaces;
using DES.Utils;

namespace DES.Classes
{
    public enum EncryptionMode
    {
        ECB,
        CBC,
        CFB,
        OFB,
        CTR,
        RD,
        RDH
    };

    public class CypherContext
    {
        private readonly byte[] _key;    
        private readonly EncryptionMode _mode;
        private readonly byte[] _initializationVector;
        private int _cutSize;
        private string _strParam;
        public ICrypto Encrypter { get; set; }

        public CypherContext(byte[] key, EncryptionMode mode, byte[] initializationVector = null, string strParam=null)
        {
            _key = key;
            _mode = mode;
            _initializationVector = initializationVector;
            _strParam = strParam;
        }

        private byte[] PaddingPkcs7(byte[] block)
        {
            byte mod = (byte)(Constants.BlockSize - block.Length % Constants.BlockSize);
            var paddedBlock = new byte[block.Length + mod];
            Array.Copy(block, paddedBlock, block.Length);
            Array.Fill(paddedBlock, mod, block.Length, mod); 
            return paddedBlock;
        }
        
        private byte[] Encrypt(byte[] block)
        {
            var resultBlock = PaddingPkcs7(block);
            _cutSize = resultBlock[^1];
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
                {
                    var curBlock = new byte[Constants.BlockSize];
                    var copyIV = new byte[8];
                    _initializationVector.CopyTo(copyIV, 0);
                    var IV = BitConverter.ToUInt64(copyIV);
                    var delta = BitConverter.ToUInt64(_initializationVector);
                    blocksList.Add(Encrypter.Encrypt(copyIV));
                    for (var i = 0; i < resultBlock.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(resultBlock, i * Constants.BlockSize, curBlock, 0, Constants.BlockSize);
                        var xorResult = BitConverter.ToUInt64(copyIV, 0) ^ BitConverter.ToUInt64(curBlock, 0);
                        blocksList.Add(Encrypter.Encrypt(BitConverter.GetBytes(xorResult)));
                        IV += delta;
                        copyIV = BitConverter.GetBytes(IV);
                    }
                    break;
                }

                case EncryptionMode.RDH:
                {
                    var curBlock = new byte[Constants.BlockSize];
                    var copyIV = new byte[8];
                    Array.Copy(_initializationVector, 0, copyIV, 0, Constants.BlockSize);
                    var IV = BitConverter.ToUInt64(copyIV);
                    var delta = BitConverter.ToUInt64(_initializationVector);
                    blocksList.Add(Encrypter.Encrypt(copyIV));
                    var xorResult = BitConverter.ToUInt64(copyIV, 0) ^
                                    BitConverter.ToUInt64(PaddingPkcs7(BitConverter.GetBytes(block.GetHashCode())));
                    blocksList.Add(BitConverter.GetBytes(xorResult));
                    for (var i = 0; i < resultBlock.Length / Constants.BlockSize; ++i)
                    {
                        IV += delta;
                        copyIV = BitConverter.GetBytes(IV);
                        Array.Copy(resultBlock, i * Constants.BlockSize, curBlock, 0, Constants.BlockSize);
                        xorResult = BitConverter.ToUInt64(copyIV, 0) ^ BitConverter.ToUInt64(curBlock);
                        blocksList.Add(Encrypter.Encrypt(BitConverter.GetBytes(xorResult)));
                    }
                    break;
                }
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(_mode), "Incorrect mode");
            }
            var connecterBlock = new byte[Constants.BlockSize * blocksList.Count];
            for (var i = 0; i < blocksList.Count; ++i)
            {
                Array.Copy(blocksList[i], 0, connecterBlock, i * Constants.BlockSize, Constants.BlockSize);
            }
            return connecterBlock;
        }
        
        private byte[] Decrypt(byte[] block)
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

                case EncryptionMode.RD:
                {
                    var curBlock = new byte[Constants.BlockSize];
                    var copyIV = new byte[8];
                    var delta = BitConverter.ToUInt64(_initializationVector);
                    Array.Copy(block, 0, curBlock, 0, Constants.BlockSize);
                    copyIV = Encrypter.Decrypt(curBlock);
                    var IV = BitConverter.ToUInt64(copyIV);
                    for (var i = 1; i < block.Length / Constants.BlockSize; ++i)
                    {
                        Array.Copy(block, i * Constants.BlockSize, curBlock, 0, Constants.BlockSize);
                        var xorResult = BitConverter.ToUInt64(Encrypter.Decrypt(curBlock), 0) ^
                                        BitConverter.ToUInt64(copyIV, 0);
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                        IV += delta;
                        copyIV = BitConverter.GetBytes(IV);
                    }
                    break;
                }
                
                case EncryptionMode.RDH:
                {
                    var curBlock = new byte[Constants.BlockSize];
                    var copyIV = new byte[8];
                    var delta = BitConverter.ToUInt64(_initializationVector);
                    Array.Copy(block, 0, curBlock, 0, Constants.BlockSize);
                    copyIV = Encrypter.Decrypt(curBlock);
                    var IV = BitConverter.ToUInt64(copyIV);
                    Array.Copy(block, 8, curBlock, 0, Constants.BlockSize);
                    var xorResult = BitConverter.ToUInt64(copyIV)
                                    ^ BitConverter.ToUInt64(PaddingPkcs7(BitConverter.GetBytes(_strParam.GetHashCode())));
                    for (var i = 2; i < block.Length / Constants.BlockSize; ++i)
                    {
                        IV += delta;
                        copyIV = BitConverter.GetBytes(IV);
                        Array.Copy(block, i * Constants.BlockSize, curBlock, 0, Constants.BlockSize);
                        xorResult = BitConverter.ToUInt64(Encrypter.Decrypt(curBlock)) ^ BitConverter.ToUInt64(copyIV);
                        blocksList.Add(BitConverter.GetBytes(xorResult));
                    }
                    break;
                }
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(_mode), "Incorrect mode");
            }
            var connectedBlock = new byte[blocksList.Count * Constants.BlockSize];
            for (var i = 0; i < blocksList.Count; ++i)
            {
                Array.Copy(blocksList[i], 0, connectedBlock,
                        i * Constants.BlockSize, Constants.BlockSize);
            }
            var returnBlock = new byte[connectedBlock.Length - _cutSize];
            Array.Copy(connectedBlock, returnBlock, returnBlock.Length);
            return returnBlock;
        }

        public void Encrypt(string originPath, string encryptedPath)
        {
            var bytes = File.ReadAllBytes(originPath);
            var encryptedBytes = Encrypt(bytes);
            File.WriteAllBytes(encryptedPath, encryptedBytes);
        }

        public void Decrypt(string originPath, string decryptedPath)
        {
            var bytes = File.ReadAllBytes(originPath);
            var decryptedBytes = Decrypt(bytes);
            File.WriteAllBytes(decryptedPath, decryptedBytes);
        }
    }
}