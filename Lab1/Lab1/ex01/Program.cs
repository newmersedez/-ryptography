using System;

namespace Lab1
{
    class Program
    {
        public static byte[] PBlock(in byte[] array, in int[] rule)
        {
            if (array.Length != rule.Length)
                throw new Exception("Length of bytes array and rule array must be the same");
            
            int tmp;
            byte[] newArray = new byte[array.Length];
            for (int i = 0; i < rule.Length; ++i)
            {
                newArray[rule[i]] = array[i];
            }
            return newArray;
        }

        static void Main(string[] args)
        {
            byte[] array = {1, 0, 0, 1};
            int[] rule = {1, 0, 2, 3};

            var newArray = PBlock(array, rule);
            foreach (var bit in newArray)
            {
                Console.WriteLine(bit);
            }
        }
    }
}