using System;
using System.Collections.Generic;
using System.Text;
using DES.Utils;

namespace DES
{
    class Program
    {
        static void Main(string[] args)
        {
            // byte[] block = BitConverter.GetBytes(98);
            // byte[] rule = {7, 3, 2, 4, 5, 6, 1};
            // byte[] newBlock = BlockUtils.PermuteBlock(block, rule);
            //
            // string blockString = Convert.ToString(BitConverter.ToInt32(block), 2).PadLeft(7, '0');
            // string newBlockString = Convert.ToString(BitConverter.ToInt32(newBlock), 2).PadLeft(7, '0');
            // Console.WriteLine(blockString);
            // Console.WriteLine(newBlockString);
            
            byte[] block = BitConverter.GetBytes(199);
            Dictionary<byte, byte> rule = new Dictionary<byte, byte>()
            {
                {0, 3},
                {1, 1},
                {2, 2},
                {3, 3}
            };
            byte[] newBlock = BlockUtils.SubstituteBlock(block, rule, 2);
            
            string blockString = Convert.ToString(BitConverter.ToInt32(block), 2).PadLeft(8, '0');
            string newBlockString = Convert.ToString(BitConverter.ToInt32(newBlock), 2).PadLeft(7, '0');
            Console.WriteLine("block     = {0}", blockString);
            Console.WriteLine("new block = {0}", newBlockString);
        }
    }
}