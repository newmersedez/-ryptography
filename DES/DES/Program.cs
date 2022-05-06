using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DES.Classes;
using DES.Utils;

namespace DES
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Permutation block test */
            // {
            //     byte[] block = BitConverter.GetBytes(98);
            //     byte[] rule = {7, 3, 2, 4, 5, 6, 1};
            //     byte[] newBlock = BlockUtils.Permute32(block, rule);
            //
            //     string blockString = Convert.ToString(BitConverter.ToInt32(block), 2).PadLeft(7, '0');
            //     string newBlockString = Convert.ToString(BitConverter.ToInt32(newBlock), 2).PadLeft(7, '0');
            //     Console.WriteLine(blockString);
            //     Console.WriteLine(newBlockString);
            // }
            
            /* Substitution block test */
            // {
            //     byte[] block = BitConverter.GetBytes(199);
            //     Dictionary<byte, byte> rule = new Dictionary<byte, byte>()
            //     {
            //         {0, 3},
            //         {1, 1},
            //         {2, 2},
            //         {3, 3}
            //     };
            //     byte[] newBlock = BlockUtils.Substitute32(block, rule, 2);
            //
            //     string blockString = Convert.ToString(BitConverter.ToInt32(block), 2).PadLeft(8, '0');
            //     string newBlockString = Convert.ToString(BitConverter.ToInt32(newBlock), 2).PadLeft(7, '0');
            //     Console.WriteLine("block     = {0}", blockString);
            //     Console.WriteLine("new block = {0}", newBlockString);
            // }

            /* Expand key class test */
            // {
            //     ExpandKey gen = new ExpandKey();
            //     byte[] key = BitConverter.GetBytes(11422891502611697239);
            //     var roundKeys = gen.GenerateRoundKeys(key);
            // }

            /* Cypher transform class test */
            // {
            //     CypherTransform transform = new CypherTransform();
            //     byte[] block = BitConverter.GetBytes(13372281337228);
            //     byte[] key = BitConverter.GetBytes(11422891502611697239);
            //     var transformedBlock = transform.Transform(block, key);
            // }
            
            /* Cypher context class test */
            // {
            //     byte[] initializationVector = {1, 1, 1, 1, 1, 1, 1, 1};
            //     string text = "lalkaxd1337228";
            //     byte[] startBlock = Encoding.ASCII.GetBytes(text);
            //
            //     CypherContext crypto = new CypherContext(BitConverter.GetBytes(11422891502611697239),
            //         EncryptionMode.ECB, initializationVector);
            //     crypto.Encrypter = new SimpleCryptoClass();
            //
            //     var encryptedBlock = crypto.Encrypt(startBlock);
            //     var decryptedBlock = crypto.Decrypt(encryptedBlock);
            //
            //     Console.WriteLine("Before = {0}", text);
            //     Console.WriteLine("Encrypted text = {0}", Encoding.UTF8.GetString(encryptedBlock));
            //     Console.WriteLine("Decrypted text = {0}", Encoding.UTF8.GetString(decryptedBlock));
            // }
            
            /* Des class test
                ECB - Arithmetic operation resulted in an overflow
                CBC - Arithmetic operation resulted in an overflow
                CFB - Works
                OFB - Works
                CTR - Works
                RD - =)
                RDH - =)
             */
            {
                byte[] initializationVector = { 1, 3, 3, 7, 1, 3, 3, 7 };
                const string text = "12345678";
                const ulong key = 1337133713371337;
                
                var crypto = new CypherContext(BitConverter.GetBytes(key), 
                    EncryptionMode.ECB, initializationVector);
                crypto.Encrypter = new Classes.DES(new ExpandKey(), new CypherTransform());
                crypto.Encrypter.GenerateRoundKeys(BitConverter.GetBytes(key));
                // crypto.Encrypter = new SimpleCryptoClass();
                
                var startBlock = Encoding.ASCII.GetBytes(text);
                var encryptedBlock = crypto.Encrypt(startBlock);
                var decryptedBlock = crypto.Decrypt(encryptedBlock);
                
                Console.WriteLine("Before = {0}", text);
                Console.WriteLine("Encrypted text = {0}", Encoding.UTF8.GetString(encryptedBlock));
                Console.WriteLine("Decrypted text = {0}", Encoding.UTF8.GetString(decryptedBlock));
            }
        }
    }
}