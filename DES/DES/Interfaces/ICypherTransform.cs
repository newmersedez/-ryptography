namespace DES.Interfaces
{
    internal interface ICypherTransform
    {
        public byte[] Transform(byte[] block, byte[] roundKey);
    }
}