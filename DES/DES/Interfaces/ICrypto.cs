namespace DES.Interfaces
{
    internal interface ICrypto
    {
        public byte[] Encrypt(byte[] block);
        public byte[] Decrypt(byte[] block);
        public void GetRoundKeys(byte[] key);
    }
}