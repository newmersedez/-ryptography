namespace DES.Interfaces
{
    public interface ICrypto
    {
        public byte[] Encrypt(byte[] block);
        public byte[] Decrypt(byte[] block);
        public void GenerateRoundKeys(byte[] key);
    }
}