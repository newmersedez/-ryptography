namespace DES.Interfaces
{
    public interface ICrypto
    {
        public byte[] Encrypt(byte[] block);
        public byte[] Decrypt(byte[] block);
        public void SetKey(byte[] key);
    }
}