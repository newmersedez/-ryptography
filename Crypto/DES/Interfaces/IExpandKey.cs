namespace DES.Interfaces
{
    public interface IExpandKey
    {
        public byte[][] GenerateRoundKeys(byte[] key);
    }
}