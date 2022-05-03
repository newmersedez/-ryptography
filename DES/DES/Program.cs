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
            // byte[] block = BitConverter.GetBytes(98);
            // byte[] rule = {7, 3, 2, 4, 5, 6, 1};
            // byte[] newBlock = BlockUtils.PermuteBlock(block, rule);
            //
            // string blockString = Convert.ToString(BitConverter.ToInt32(block), 2).PadLeft(7, '0');
            // string newBlockString = Convert.ToString(BitConverter.ToInt32(newBlock), 2).PadLeft(7, '0');
            // Console.WriteLine(blockString);
            // Console.WriteLine(newBlockString);
            
            /* Substitution block test */
            // byte[] block = BitConverter.GetBytes(199);
            // Dictionary<byte, byte> rule = new Dictionary<byte, byte>()
            // {
            //     {0, 3},
            //     {1, 1},
            //     {2, 2},
            //     {3, 3}
            // };
            // byte[] newBlock = BlockUtils.SubstituteBlock(block, rule, 2);
            //
            // string blockString = Convert.ToString(BitConverter.ToInt32(block), 2).PadLeft(8, '0');
            // string newBlockString = Convert.ToString(BitConverter.ToInt32(newBlock), 2).PadLeft(7, '0');
            // Console.WriteLine("block     = {0}", blockString);
            // Console.WriteLine("new block = {0}", newBlockString);

            /* Expand key class test */
            // ExpandKey gen = new ExpandKey();
            // byte[] key = BitConverter.GetBytes(11422891502611697239);
            // var roundKeys = gen.GenerateRoundKeys(key);

            /* Cypher transform class test */
            // CypherTransform transform = new CypherTransform();
            // byte[] block = BitConverter.GetBytes(13372281337228);
            // byte[] key = BitConverter.GetBytes(11422891502611697239);
            // var transformedBlock = transform.Transform(block, key);
            
            /* Cypher context class test */
            byte[] block = BitConverter.GetBytes(123123123);
            byte[] key = BitConverter.GetBytes(11422891502611697239);
            CypherContext cypherEncrypter = new CypherContext(key, EncryptionMode.ECB);
            var encryptedBlock = cypherEncrypter.Encrypt(block);
        }
    }
}