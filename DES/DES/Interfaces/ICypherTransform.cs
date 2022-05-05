namespace DES.Interfaces
{
    public interface ICypherTransform
    {
        public byte[] Transform(byte[] block, byte[] roundKey);
    }
}