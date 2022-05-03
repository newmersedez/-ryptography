namespace DES.Interfaces
{
    internal interface IExpandKey
    {
        public byte[][] GenerateRoundKeys(byte[] key);
    }
}